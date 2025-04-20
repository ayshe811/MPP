using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour
{
    public ParticleSystem parSystem;
    ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule;
    playerScript playScript;
    // Start is called before the first frame update
    void Start()
    {
        parSystem = GetComponent<ParticleSystem>();
        playScript = GameObject.Find("player").GetComponent<playerScript>();
        velocityOverLifetimeModule = parSystem.velocityOverLifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playScript.xInput == 0) velocityOverLifetimeModule.x = 0;
        else if (playScript.xInput > 0) velocityOverLifetimeModule.x = -.15f;
        else if (playScript.xInput < 0) velocityOverLifetimeModule.x = .15f;
    }

    public void DecreaseEmissions()
    {
        ParticleSystem.EmissionModule emission = parSystem.emission;
        ParticleSystem.MinMaxCurve emissionRate = emission.rateOverTime;

        float newMin = emissionRate.constantMin - 12;
        float newMAx = emissionRate.constantMax - 8;

        emissionRate = new ParticleSystem.MinMaxCurve(newMin, newMAx);
    }
}
