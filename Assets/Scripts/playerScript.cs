using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public ScreenShake screenShake;
    Rigidbody2D rb;
   [SerializeField] float xInput, yInput, scale, timer;
    float playerSpeed = 9, sizeX, sizeY, sizeDecRate, speedIncRate;
    int weightPoints, level;
    public bool isHit;
    public GameObject canvas, poison;
    gameManager gameManager;
    collisionManager collisionManager;
    spawnerScript spawner;
    public AudioSource src;
    public spawnerScript spawner2, spawner3;
    Color color;
    public int correctCollision = 0, combo, previousScore;
    [SerializeField] bool metCollisionTarget = false;
    public int score, sizePoints;
    public bool tabShowed, hasStarted;
    public TextMeshProUGUI comboMeter;
    public audioScript audioScript;

    SpriteRenderer sr;
    public Color glowColour;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
        spawner = GameObject.Find("dummy1").GetComponent<spawnerScript>();
        glowColour = GetComponent<SpriteRenderer>().color;
        sr = GetComponent<SpriteRenderer>();
        hasStarted = false;
        previousScore = 0;

        sizeX = 1; sizeY = 1;
        combo = 1;
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

    private void Update()
    {
        if (combo == 1) comboMeter.text = null;
        else comboMeter.text = "x " + combo;

        src.volume = 1;
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
                    Debug.Log("Correct Collision!");

                    combo++;
                    gameManager.gameTimer += 5;
                    collisionManager.OnCorrectCollision();
                    if (!hasStarted)
                    {
                        if (spawner.currentTechLevel < 3) spawner.currentTechLevel++;
                        if (spawner.currentTechLevel >= 3)
                        {
                            hasStarted = true;
                            StopCoroutine(spawner.beforeGame());
                            StartCoroutine(spawner.techSpawn());
                        //    StartCoroutine(spawner.otherSpawn());
                            gameManager.states = gameManager.gameState.playable;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Incorrect Collision!");

                    combo = 1;
                    collisionManager.previousValue = 0;
                    spawner.secondSpawm = .5f;
                    spawner.spawnValue = 0;
                    gameManager.playerLives--; // five playerLives
                    screenShake.TriggerShake();
                    src.PlayOneShot(src.clip);
                    audioScript.src.Stop();
                }
            }
        }
        if (collision.gameObject.CompareTag("Poison"))
        {
            // player freeze (MORE FEEDBACK TO INFORM THE PLAYER)!!
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
    void GlowEffect()
    {
        float glowDuration = 1f, glowIntensity = 1f;
        LeanTween.value(gameObject, 0f, glowIntensity, glowDuration)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong() // Makes it pulse back and forth
            .setOnUpdate((float value) =>
            {
                Color newColor = glowColour;
                newColor.a = value;
                sr.color = newColor;
            });
    }
    void FixedUpdate() // player movement
    {
        xInput = Input.GetAxisRaw("Horizontal");
        //   yInput = Input.GetAxisRaw("Vertical");

        //GlowEffect();

        rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);
        Vector2 targetVelocity = new Vector2(rb.velocity.x, 4);
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.fixedDeltaTime);

        //if (gameManager.states == gameManager.gameState.onboadring) rb.velocity = new Vector2(xInput * playerSpeed, yInput * playerSpeed);
        //else if (gameManager.states == gameManager.gameState.playable)
        //{
        //    rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);
        //    Vector2 targetVelocity = new Vector2(rb.velocity.x, 4);
        //    rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.fixedDeltaTime);
        //}

        if (transform.position.x <= -2.7f) transform.position = new Vector3(-2.7f, transform.position.y);
        if (transform.position.x >= 2.7f) transform.position = new Vector3(2.7f, transform.position.y);
    }
}
