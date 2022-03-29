using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenOnKill : HitNotifier, LateInitializable, Dependency<TimeScaleManager>, Dependency<FollowTransform>
{
    private FollowTransform Camera;
    private TimeScaleManager TimeScaleManager;
    [SerializeField] float shakeLength;
    [SerializeField] float shakeAmt;
    [SerializeField] float stopLength;
    [SerializeField] bool stopOnHit;
    [SerializeField] bool stopOnKill;
    [SerializeField] bool shakeOnHit;
    [SerializeField] bool stopOnDamage;

    [SerializeField] HealthManager hm;

    public void InjectDependency(TimeScaleManager tms)
    {
        TimeScaleManager = tms;
    }

    public void InjectDependency(FollowTransform ft)
    {
        Camera = ft; 
    }

    public void LateInit()
    {
        hm.DamageTaken += DamageTaken;
    }

    private void DamageTaken()
    {
        if (stopOnDamage)
        {
            TimeScaleManager.BeginFreeze(stopLength);
        }
    }

    public override void OnHit(Targetable hit)
    {
        if (stopOnHit)
        {
            TimeScaleManager.BeginFreeze(stopLength);
        }

        if (shakeOnHit)
        {
            Camera.ApplyShake(shakeAmt, shakeLength);
        }

        if (hit != null && !hit.IsAlive() && stopOnKill)
        {
            TimeScaleManager.BeginFreeze(stopLength);
        }
    }
}
