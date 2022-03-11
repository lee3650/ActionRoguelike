using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenOnHit : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager hm;
    [SerializeField] FollowTransform Camera;
    [SerializeField] float shakeAmt;
    [SerializeField] float shakeLength;

    public void LateInit()
    {
        hm.DamageTaken += DamageTaken;
    }

    private void DamageTaken()
    {
        Camera.ApplyShake(shakeAmt, shakeLength);
    }
}
