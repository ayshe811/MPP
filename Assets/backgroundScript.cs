using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class backgroundScript : MonoBehaviour
{
    public float scrollSpeed; // Set per layer (fast for foreground, slow for background).
    private Vector2 startPos;
    private float length;

    GameObject player;
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.y;

        player = GameObject.Find("player");
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y);
        //// Move downward (negative y-axis) at assigned speed.
        //transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        //// Optional: Infinite looping (reposition when off-screen).
        //if (transform.position.y < startPos.y - length)
        //{
        //    transform.position = startPos;
        //}
    }
}
