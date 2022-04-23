using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrackingOnDamage : MonoBehaviour, SubEntity
{
    [SerializeField] FollowTarget FollowTarget;
    [SerializeField] HealthManager hm;

    private float lastBounce = 0f; 

    public void HandleEvent(GameEvent e)
    {
        if (Time.realtimeSinceStartup - lastBounce > 1.25f * Time.fixedDeltaTime)
        {
            print(string.Format("Current realtime: {0}, last bounce: {1}, delta: {2}, 1.25f * fixed: {3}", Time.realtimeSinceStartup, lastBounce,
                Time.realtimeSinceStartup - lastBounce, 1.25f * Time.fixedDeltaTime));
            FollowTarget.BounceBack(hm.IsAlive());
            lastBounce = Time.realtimeSinceStartup;
        }
    }
}
