using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenOnHit : MonoBehaviour, LateInitializable, Dependency<FollowTransform>
{
    [SerializeField] HealthManager hm;
    [SerializeField] FollowTransform Camera;
    [SerializeField] float shakeAmt;
    [SerializeField] float shakeLength;

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
        Camera.ApplyShake(shakeAmt, shakeLength);
    }
}
