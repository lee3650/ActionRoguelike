using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBox : MonoBehaviour, SubEntity
{
    [SerializeField] SettableCurrentTarget SettableCurrentTarget;
    [SerializeField] Projectile p;
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] float delay; 

    public void HandleEvent(GameEvent e)
    {
        StartCoroutine(LaunchProjectile(e.Sender));
    }

    IEnumerator LaunchProjectile(Targetable t)
    {
        yield return new WaitForSeconds(delay);
        SettableCurrentTarget.SetCurrentTarget(t);
        TimerProjectile.LaunchProjectile(p, transform.position, transform.up, InjectionSet);
    }
}
