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
    // Start is called before the first frame update
    void Start()
    {
        particleScript = GameObject.Find("Particle Manager").GetComponent<particleManager>();
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
