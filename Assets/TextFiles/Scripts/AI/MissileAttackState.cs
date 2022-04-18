using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackState : State
{
    [SerializeField] CurrentTarget CurrentTarget;
    [SerializeField] State NextState;
    [SerializeField] Projectile Projectile;
    [SerializeField] float stateLength = 1f;
    [SerializeField] InjectionSet InjectionSet;

    private float timer = 0f;

    public override void EnterState()
    {
        timer = 0f;

        Projectile p = Instantiate(Projectile, transform.position, Quaternion.identity);
        InjectionSet.InjectDependencies(p.transform);
        p.Launch();
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= stateLength)
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {

    }
}
