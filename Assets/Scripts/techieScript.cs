using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class techieScript : MonoBehaviour
{
    public string color;
    public GameObject cursor;
    playerScript player;
    public Color highlight;
    public AudioSource src;

    public SpriteRenderer sr;
    public PolygonCollider2D poly2d;

     particleManager parScript;

    public Rigidbody2D rb;

   // [SerializeField] bool isHit;
    void Start()
    {
        Application.targetFrameRate = 60;
        parScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
    }


    void Update()
    {
        transform.Rotate(Vector3.back * .7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // add selective collisions!!  
        {

            player = collision.gameObject.GetComponent<playerScript>();
            player.isHit = true;

            if (color == "Red") Instantiate(parScript.particlePrefab[0], transform.position, Quaternion.identity);
            if (color == "Yellow") Instantiate(parScript.particlePrefab[1], transform.position, Quaternion.identity);
            if (color == "Pink") Instantiate(parScript.particlePrefab[2], transform.position, Quaternion.identity);
            if (color == "Purple") Instantiate(parScript.particlePrefab[3], transform.position, Quaternion.identity);
            if (color == "Green") Instantiate(parScript.particlePrefab[4], transform.position, Quaternion.identity);

            src.PlayOneShot(src.clip);
            poly2d.enabled = false;
            sr.enabled = false;
            Destroy(gameObject, 0.5f);
            player.IncreaseSize();      
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
