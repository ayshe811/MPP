using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    GameObject cursor;
    Color highlight; 
    void Start()
    {
        cursor = GameObject.Find("cursor");
    }

    void Update()
    {
        transform.Rotate(Vector3.back * 1.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "mouse")
        {
            print("hit!");
            transform.localScale = new Vector3(0.2f, 0.2f);
           // highlight = new Color()
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "mouse") transform.localScale = new Vector3(0.13f, 0.13f);
    }
}
