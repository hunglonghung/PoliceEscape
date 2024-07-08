using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelParticleRenderer : MonoBehaviour
{
 PlayerController playerController;
ParticleSystem particleSystemSmoke;
ParticleSystem.EmissionModule particleSystemEmissionModule;
float particleEmissionRate;

// Awake is called when the script instance is being loaded.
 void Awake()
{
    // Get the top down car controller
    playerController = GetComponentInParent<PlayerController>();

    // Get the particle system
    particleSystemSmoke = GetComponent<ParticleSystem>();

    // Get the emission component
    particleSystemEmissionModule = particleSystemSmoke.emission;

    // Set it to zero emission.
    particleSystemEmissionModule.rateOverTime = 0;
}
void Update()
{
    // Reduce the particles over time.
    particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
    particleSystemEmissionModule.rateOverTime = particleEmissionRate;

    if (playerController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
    {
        // If the car tires are screeching then we'll emit smoke. If the player is braking then emit a lot of smoke.
        if (isBraking)
            particleEmissionRate = 30;
        // If the player is drifting we'll emit smoke based on how much the player is drifting.
        else 
            particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
    }
}


}