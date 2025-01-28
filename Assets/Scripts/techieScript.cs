using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    public GameObject ParticleManager;
    public string color;
    GameObject cursor;
    Color highlight;
    AudioSource src;
    ParticleSystem parSystem;
    SpriteRenderer sr;
    PolygonCollider2D poly2d;

    Rigidbody2D rb;

    public bool isHit;
    void Start()
    {
        cursor = GameObject.Find("cursor");
        src = GetComponent<AudioSource>();
        parSystem = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        poly2d = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        ParticleManager = GameObject.Find("Particle Manager");

        isHit = false;
    }


    void Update()
    {
      transform.Rotate(Vector3.back * .7f);


        //if (color == "Red") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[0],
        //    transform.position, Quaternion.identity);
        //if (color == "Yellow") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[1],
        //   transform.position, Quaternion.identity);
        //if (color == "Pink") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[2],
        //   transform.position, Quaternion.identity);
        //if (color == "Purple") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[3],
        //   transform.position, Quaternion.identity);
        //if (color == "Green") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[4],
        //   transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (color == "Red") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[0],
               transform.position, Quaternion.identity);
            if (color == "Yellow") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[1],
               transform.position, Quaternion.identity);
            if (color == "Pink") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[2],
               transform.position, Quaternion.identity);
            if (color == "Purple") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[3],
               transform.position, Quaternion.identity);
            if (color == "Green") Instantiate(ParticleManager.GetComponent<particleManager>().particlePrefab[4],
               transform.position, Quaternion.identity);

            isHit = true;
            src.PlayOneShot(src.clip);
            //parSystem.Play();
            poly2d.enabled = false;
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
