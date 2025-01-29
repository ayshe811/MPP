using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput, yInput;
    [SerializeField] float playerSpeed; 

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

        //testing if karken/hub are compatible!

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (playState == playerStates.distracted) playerSpeed = 9;
        else playerSpeed = 4;

        rb.velocity = new Vector2(xInput * playerSpeed, yInput * playerSpeed);
    }
}
