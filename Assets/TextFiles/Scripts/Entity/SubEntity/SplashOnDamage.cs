using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashOnDamage : MonoBehaviour, SubEntity
{
    [SerializeField] Rigidbody2D prefab;
    [SerializeField] float velocity = 50;
    [SerializeField] float angleAdjustment = 0.3f; 
    [SerializeField] float velocityAdjustment = 0.1f;
    [SerializeField] float radius = 1f;

    public void HandleEvent(GameEvent e)
    {
        if (!SpraySetting.EnableSpray)
        {
            return; 
        }

        switch (e.Type)
        {
            case SignalType.Physical:
                Vector2 baseDir = (Vector2)transform.position - e.Sender.GetMyPosition();

                baseDir.Normalize();

                int numParticles = 10; 

                for (int i = 0; i < numParticles; i++)
                {
                    Rigidbody2D particle = Instantiate<Rigidbody2D>(prefab, (Vector2)transform.position + (Random.insideUnitCircle * radius), Quaternion.identity);
                    particle.velocity = (baseDir + new Vector2(Random.Range(-angleAdjustment, angleAdjustment), Random.Range(-angleAdjustment, angleAdjustment))).normalized
                        * Random.Range(velocity - velocityAdjustment, velocity + velocityAdjustment);
                }

                break;
        }
    }
}
