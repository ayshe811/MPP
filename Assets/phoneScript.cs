using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneScript : MonoBehaviour
{

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y);
    }
}
