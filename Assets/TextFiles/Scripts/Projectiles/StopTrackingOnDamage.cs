using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrackingOnDamage : MonoBehaviour, LateInitializable
{
    [SerializeField] FollowTarget FollowTarget;
    [SerializeField] HealthManager hm;

    public void LateInit()
    {
        hm.DamageTaken += DamageTaken;
    }

    private void DamageTaken()
    {
        FollowTarget.BounceBack(hm.IsAlive());
    }
}
