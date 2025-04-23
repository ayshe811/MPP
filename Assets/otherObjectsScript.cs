using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherObjectsScript : MonoBehaviour
{
    particleManager particleScript;
    [SerializeField] bool red;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] PolygonCollider2D poly2D;
    [SerializeField] ParticleSystem parSystem;
    AudioSource src;
    Rigidbody2D rb;
    [SerializeField] float zigSpeed, directionChange = 1;
    private Vector2 currentDirection;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        particleScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
        src = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        newDirection();
    }
    private void Update()
    {
        //  src.Play();
        //float x = Mathf.PerlinNoise(Time.time * speed, 0) * 2 - 1; // -1 to 1 range
        //float y = Mathf.PerlinNoise(0, Time.time * speed) * 2 - 1;
        //transform.position += new Vector3(x, y, 0) * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= directionChange)
        {
            newDirection();
            timer = 0;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(currentDirection * zigSpeed);
    }

    void newDirection()
    {
        float angle = Random.Range(45f, 135f);
        currentDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Rad2Deg)).normalized;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (red) Instantiate(particleScript.particlePrefab[0], transform.position, Quaternion.identity);
            else Instantiate(particleScript.particlePrefab[4], transform.position, Quaternion.identity);

            sr.enabled = false; poly2D.enabled = false; parSystem.Clear();
            Destroy(gameObject, .2f);
        }
    }
}
