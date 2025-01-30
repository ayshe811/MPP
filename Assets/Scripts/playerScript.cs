using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput, yInput, sizeX, sizeY;
    public float playerSpeed;

    public bool isHit;
    gameManager gameManager;

    public enum playerStates { mindfulness, distracted }
    public playerStates playState;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        playState = playerStates.distracted;

        if (playState == playerStates.distracted) playerSpeed = 9;
        else playerSpeed = 4;

        sizeX = 1; sizeY = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playState == playerStates.mindfulness) isHit = false;
        if (!isHit && playState == playerStates.mindfulness)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f);
            sizeX -= 0.01f; sizeY -= 0.01f;
            playerSpeed += 0.02f;

            if (sizeX <= 1 && sizeY <= 1) transform.localScale = Vector3.one;
            if (playerSpeed >= 9) playerSpeed = 9;
        }
    }
    void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(xInput * playerSpeed, yInput * playerSpeed);
    }
}
