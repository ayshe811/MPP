using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput, yInput, scale, timer;
    public float playerSpeed, sizeX, sizeY, sizeDecRate, speedIncRate;
    int weightPoints, level;
    public bool isHit;
    public GameObject canvas;
    gameManager gameManager;
    collisionManager collisionManager;
    spawnerScript spawner;
    Color color;
    public TMP_Text scoreText;
    public int correctCollision = 0;
    [SerializeField] bool metCollisionTarget = false;

    public enum playerStates { mindfulness, distracted }
    public playerStates playState;
    public int score, sizePoints;
    public bool tabShowed, hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
        spawner = GameObject.Find("spawner1").GetComponent<spawnerScript>();
        color = GetComponent<SpriteRenderer>().color;
        playState = playerStates.distracted;
        if (playState == playerStates.distracted) playerSpeed = 9;
        hasStarted = false;

        sizeX = 1; sizeY = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "" + score;
        if (Input.GetKeyDown(KeyCode.Tab) && playState == playerStates.distracted) playState = playerStates.mindfulness;
        else if (Input.GetKeyDown(KeyCode.Tab) && playState == playerStates.mindfulness) playState = playerStates.distracted;
        if (playState == playerStates.mindfulness)
        {
            isHit = false;
            gameManager.tabTimer = 0;
            tabShowed = true;
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                playState = playerStates.distracted;
                timer = 0;
            }
        }
        if (!isHit && playState == playerStates.mindfulness) DecreaseSize();
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
                    score += 2;
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
        yInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(xInput * playerSpeed, yInput * playerSpeed);
    }
}
