using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmissionStopper : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    
    public void StopAllParticleEmission()
    {
        foreach(var ps in particleSystems)
        {
            var em = ps.emission;
            em.enabled = false;
        }
    }
}
