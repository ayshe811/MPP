using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    GameObject cursor;
    Color highlight;

    AudioSource src;
    ParticleSystem parSystem;
    SpriteRenderer sr;
    void Start()
    {
        cursor = GameObject.Find("cursor");
        src = GetComponent<AudioSource>();
        parSystem = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        transform.Rotate(Vector3.back * 1.3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            src.PlayOneShot(src.clip);
            parSystem.Play();
            sr.enabled = false;
            Destroy(gameObject, 0.5f);

        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.tag == "mouse")
        //    {
        //        print("hit!");
        //        transform.localScale = new Vector3(0.2f, 0.2f);
        //       // highlight = new Color()
        //    }

        //    if (collision.gameObject.tag == "Player") src.PlayOneShot(src.clip);
        //}
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.gameObject.tag == "mouse") transform.localScale = new Vector3(0.13f, 0.13f);
    }
}
