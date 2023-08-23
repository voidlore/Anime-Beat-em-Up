using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemController : MonoBehaviour
{
    public float emissionRate;
    private ParticleSystem pSystem;
    private ParticleSystem.EmissionModule emissionModule;

    private void Start()
    {
        if (pSystem == null)
            pSystem = GetComponent<ParticleSystem>();
        emissionModule = pSystem.emission;
    }

    private void Update()
    {
        emissionModule.rateOverTime = emissionRate;
    }
}
