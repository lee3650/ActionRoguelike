using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenOnKill : HitNotifier
{
    [SerializeField] FollowTransform Camera;
    [SerializeField] float shakeLength;
    [SerializeField] float shakeAmt;

    public override void OnHit(Targetable hit)
    {
        if (hit != null && !hit.IsAlive())
        {
            Camera.ApplyShake(shakeAmt, shakeLength);
        }
    }
}
