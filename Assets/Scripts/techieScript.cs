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
    gameManager gameManager;
    Rigidbody2D rb;
    GameObject player;
    [SerializeField] Color glowColour;
    collisionManager collisionManager;

    void Start()
    {
        Application.targetFrameRate = 60;
        parScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
        spaScript = GameObject.Find("dummy1").GetComponent<spawnerScript>();
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<collisionManager>();
        player = GameObject.Find("player");
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(Vector3.back * .7f);
        if (!playScript.hasStarted) rb.gravityScale = .085f;
        else rb.gravityScale = .09f;
    //    src.volume = .6f;

        //   GlowEffect();

        //if (gameManager.states == gameManager.gameState.onboadring)
        //{
        //    if (transform.position.y <= 2.5f) transform.position = new Vector3(transform.position.x, 2.5f);
        //}
        //else if (gameManager.states == gameManager.gameState.playable) transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    void GlowEffect()
    {
        float glowDuration = 1f, glowIntensity = 1f;
        LeanTween.value(gameObject, 0f, glowIntensity, glowDuration)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong() // Makes it pulse back and forth
            .setOnUpdate((float value) =>
            {
                Color newColor = glowColour;
                newColor.a = value;
                sr.color = newColor;
            });
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
            if (color == "Blue") Instantiate(parScript.particlePrefab[3], transform.position, Quaternion.identity);
            if (color == "Green") Instantiate(parScript.particlePrefab[4], transform.position, Quaternion.identity);
            if (color == "Lime") Instantiate(parScript.particlePrefab[5], transform.position, Quaternion.identity);
            if (color == "Purple") Instantiate(parScript.particlePrefab[6], transform.position, Quaternion.identity);
            if (color == "Orange") Instantiate(parScript.particlePrefab[7], transform.position, Quaternion.identity);

            //src.PlayOneShot(src.clip);
            poly2d.enabled = false;
            sr.enabled = false;
            Destroy(gameObject, 0.5f);
            //  player.IncreaseSize();

            if (GetComponent<techieScript>().color ==
                    collisionManager.objectsInSequence[collisionManager.currentIndex].GetComponent<techieScript>().color)
            {
                gameManager.src.PlayOneShot(src.clip);
                poly2d.enabled = false;
                sr.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}

