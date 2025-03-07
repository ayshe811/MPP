using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    public string color;
    public GameObject cursor;
    public Color highlight;
    public AudioSource src;
    public SpriteRenderer sr;
    public PolygonCollider2D poly2d;
    particleManager parScript;
    spawnerScript spaScript;
    playerScript playScript;
    Rigidbody2D rb;

    void Start()
    {
        Application.targetFrameRate = 60;
        parScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
        spaScript = GameObject.Find("spawner1").GetComponent<spawnerScript>();
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(Vector3.back * .7f);
        if (!playScript.hasStarted) rb.gravityScale = .12f;
        else rb.gravityScale = .18f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playScript = collision.gameObject.GetComponent<playerScript>();
            playScript.isHit = true;

            if (color == "Red") Instantiate(parScript.particlePrefab[0], transform.position, Quaternion.identity);
            if (color == "Yellow") Instantiate(parScript.particlePrefab[1], transform.position, Quaternion.identity);
            if (color == "Pink") Instantiate(parScript.particlePrefab[2], transform.position, Quaternion.identity);
            if (color == "Purple") Instantiate(parScript.particlePrefab[3], transform.position, Quaternion.identity);
            if (color == "Green") Instantiate(parScript.particlePrefab[4], transform.position, Quaternion.identity);

            src.PlayOneShot(src.clip);
            poly2d.enabled = false;
            sr.enabled = false;
            Destroy(gameObject, 0.5f);
          //  player.IncreaseSize();
        }
    }
}

