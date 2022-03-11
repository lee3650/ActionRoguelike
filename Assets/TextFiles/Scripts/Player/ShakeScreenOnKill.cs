using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenOnKill : HitNotifier, LateInitializable
{
    [SerializeField] FollowTransform Camera;
    [SerializeField] float shakeLength;
    [SerializeField] float shakeAmt;
    [SerializeField] float timeScale;
    [SerializeField] float stopLength;
    [SerializeField] bool stopOnKill;
    [SerializeField] bool shakeOnHit;
    [SerializeField] bool stopOnDamage;

    [SerializeField] HealthManager hm;

    public void LateInit()
    {
        hm.DamageTaken += DamageTaken;
    }

    private void DamageTaken()
    {
        if (stopOnDamage)
        {
            StopAllCoroutines();
            StartCoroutine(ScreenStop());
        }
    }

    public override void OnHit(Targetable hit)
    {
        if (hit != null && !hit.IsAlive())
        {
            if (stopOnKill)
            {
                StopAllCoroutines();
                StartCoroutine(ScreenStop());
            }

            if (shakeOnHit)
            {
                Camera.ApplyShake(shakeAmt, shakeLength);
            }
        }
    }

    IEnumerator ScreenStop()
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(stopLength);
        Time.timeScale = 1;
    }
}
