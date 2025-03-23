using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
   [SerializeField] float xInput, yInput, scale, timer;
    float playerSpeed = 9, sizeX, sizeY, sizeDecRate, speedIncRate;
    int weightPoints, level, combo;
    public bool isHit;
    public GameObject canvas;
    gameManager gameManager;
    collisionManager collisionManager;
    spawnerScript spawner;
    public spawnerScript spawner2, spawner3;
    Color color;
    public int correctCollision = 0;
    [SerializeField] bool metCollisionTarget = false;
    public int score, sizePoints;
    public bool tabShowed, hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
        spawner = GameObject.Find("dummy1").GetComponent<spawnerScript>();
        color = GetComponent<SpriteRenderer>().color;
        hasStarted = false;

        sizeX = 1; sizeY = 1;
        combo = 0;
    }
    public void DecreaseSize()
    {
        transform.localScale -= new Vector3(0.005f, 0.005f);
        sizeX -= sizeDecRate; sizeY -= sizeDecRate;
        playerSpeed += speedIncRate;
        collisionManager.currentIndex = 0;
        if (sizeX <= 1.1f && sizeY <= 1.1f)
        {
            transform.localScale = Vector3.one;
            sizeX = 1; sizeY = 1;
            playerSpeed = 9;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            if (collisionManager.queue.Count > 0)
            {
                score++;
                if (collision.gameObject.GetComponent<techieScript>().color ==
                    collisionManager.objectsInSequence[collisionManager.currentIndex].GetComponent<techieScript>().color)
                {
                    combo++;
                    gameManager.gameTimer += 5;
                    Debug.Log("Correct Collision!");
                    collisionManager.OnCorrectCollision();
                    if (!hasStarted)
                    {
                        if (spawner.currentTechLevel < 2)
                        {
                            spawner.currentTechLevel++;
                            StartCoroutine(spawner.beforeGame());
                        }
                        else if (spawner.currentTechLevel == 2)
                        {
                            hasStarted = true;
                            StartCoroutine(spawner.techSpawn());
                        }
                    }
                }
                else
                {
                    combo = 0;
                    gameManager.gameTimer -= 30;
                    Debug.LogWarning("Incorrect Collision!");
                }
            }
        }
    }
    public void IncreaseSize() // maybe? maybe not? (i don't fully understand whats happening here)
    {
        sizePoints++;

        sizePoints = 0;
        transform.localScale += new Vector3(0.01f, 0.01f);
        sizeX += 0.01f;
        sizeY += 0.01f;
        playerSpeed -= 0.1f;
    }
    void FixedUpdate() // player movement
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);
        Vector2 targetVelocity = new Vector2(rb.velocity.x, 4);
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.fixedDeltaTime);
    }
}
