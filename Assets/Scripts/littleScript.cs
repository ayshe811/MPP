using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleScript : MonoBehaviour
{
     private ParticleSystem parSystem;

    // Start is called before the first frame update
    void Start()
    {
        parSystem = GetComponent<ParticleSystem>();
        
        parSystem.Play();
        Destroy(gameObject, 0.5f);
    }
}
