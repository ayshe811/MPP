using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleScript : MonoBehaviour
{
     ParticleSystem parSystem;

    // Start is called before the first frame update
    void Start()
    {

        parSystem = GetComponent<ParticleSystem>();
        parSystem.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
