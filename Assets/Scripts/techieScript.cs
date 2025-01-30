using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    public string color;
    GameObject cursor;
    GameObject player;
    Color highlight;
    AudioSource src;
    ParticleSystem parSystem;
    SpriteRenderer sr;
    PolygonCollider2D poly2d;

    particleManager parScript;

    Rigidbody2D rb;

   // [SerializeField] bool isHit;
    void Start()
    {
        Application.targetFrameRate = 60;

        cursor = GameObject.Find("cursor");
        player = GameObject.Find("player");
        src = GetComponent<AudioSource>();
        parSystem = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        poly2d = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        parScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
    }


    void Update()
    {
        transform.Rotate(Vector3.back * .7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<playerScript>().isHit = true;

            if (color == "Red") Instantiate(parScript.particlePrefab[0], transform.position, Quaternion.identity);
            if (color == "Yellow") Instantiate(parScript.particlePrefab[1], transform.position, Quaternion.identity);
            if (color == "Pink") Instantiate(parScript.particlePrefab[2], transform.position, Quaternion.identity);
            if (color == "Purple") Instantiate(parScript.particlePrefab[3], transform.position, Quaternion.identity);
            if (color == "Green") Instantiate(parScript.particlePrefab[4], transform.position, Quaternion.identity);

            src.PlayOneShot(src.clip);
            poly2d.enabled = false;
            sr.enabled = false;
            Destroy(gameObject, 0.5f);

            player.transform.localScale += new Vector3(0.01f, 0.01f);
            player.GetComponent<playerScript>().playerSpeed -= 0.2f ;
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

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player") isHit = true;
    //}
}
