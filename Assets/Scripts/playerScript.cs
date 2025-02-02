using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput, yInput, scale;
    public float playerSpeed, sizeX, sizeY, sizeDecRate, speedIncRate;
    int weightPoints,level;
    public bool isHit;
    gameManager gameManager;
    collisionManager collisionManager;


    public enum playerStates { mindfulness, distracted }
    public playerStates playState;
    int sizePoints; 
    // Start is called before the first frame update
    void Start()
    {       
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
        playState = playerStates.distracted;
        sizeDecRate = 0.005f;
        speedIncRate = 0.01f;
        if (playState == playerStates.distracted) playerSpeed = 9;

        sizeX = 1; sizeY = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && playState == playerStates.distracted) playState = playerStates.mindfulness;
        else if (Input.GetKeyDown(KeyCode.Tab) && playState == playerStates.mindfulness) playState = playerStates.distracted;
        if (playState == playerStates.mindfulness) isHit = false;
        if (!isHit && playState == playerStates.mindfulness)
        {
            transform.localScale -= new Vector3(0.005f, 0.005f);
            sizeX -= sizeDecRate; sizeY -= sizeDecRate;
            playerSpeed += speedIncRate;

            if (sizeX <= 1.1f && sizeY <= 1.1f)
            {
                transform.localScale = Vector3.one;
                sizeX = 1; sizeY = 1;

                playerSpeed = 9;
            }
        }
        //player is in a state of mindfulness for a short period of time, prompting the player to focus before heading back to a state of distraction
        if (transform.localScale == Vector3.one && playerSpeed == 9) playState = playerStates.distracted;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collisionManager.queue.Count > 0)
        {
            GameObject expectedObject = collisionManager.queue.Peek();
            if (collision.gameObject == expectedObject)
            {
                Debug.Log("Correct Collision!");

                collisionManager.queue.Dequeue();
                collisionManager.currentIndex++;
            }
            else Debug.LogWarning("Incorrect Collision!");
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
