using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalParticles : MonoBehaviour, SubEntity
{
    [SerializeField] ParticleSystem System;
    [SerializeField] SignalType ActiveType;
    [SerializeField] float lifespan = 0.8f;
    [SerializeField] bool lifespanIsMagnitude; 
    public void HandleEvent(GameEvent e)
    {
        if (e.Type == ActiveType)
        {
            if (lifespanIsMagnitude)
            {
                lifespan = e.Magnitude; 
            }

            StopAllCoroutines();
            System.Play();
            StartCoroutine(StopParticles());
        }
    }

    IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(lifespan);
        System.Stop();
    }
}
