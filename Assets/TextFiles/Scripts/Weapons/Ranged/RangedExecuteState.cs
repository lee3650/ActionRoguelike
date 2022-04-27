using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedExecuteState : State, Dependency<HandAndArmGetter>
{
    private HandAndArmGetter HandAndArmGetter;

    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] Projectile MyProjectile;
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] State NextState;
    [SerializeField] float ExecuteLength;

    float timer = 0f; 

    public void InjectDependency(HandAndArmGetter h)
    {
        HandAndArmGetter = h;
    }

    public override void EnterState()
    {
        MyWeapon.SetAttackStage(AttackStage.Execution);

        Projectile instance = Instantiate<Projectile>(MyProjectile, transform.position, transform.rotation);
        InjectionSet.InjectDependencies(instance.transform);
        instance.Launch();

        HandAndArmGetter.ResetHandPosition();

        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime; 
        if (timer > ExecuteLength)
        {
            StateController.EnterState(NextState);
        }
    }
    public override void ExitState()
    {
        MyWeapon.SetAttackStage(AttackStage.Recovery);
        MyWeapon.FinishedAttack();
    }
}
