using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemController : MonoBehaviour
{
    public Percentage emissionRate;
    private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    private void Start()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();
        emissionModule = particleSystem.emission;
    }

    private void Update()
    {
        emissionModule.rateOverTime = emissionRate.percentage;
    }
}
