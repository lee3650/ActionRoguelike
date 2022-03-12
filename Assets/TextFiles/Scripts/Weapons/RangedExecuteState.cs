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

        StateController.EnterState(NextState);
    }

    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
        HandAndArmGetter.ResetHandPosition();
        MyWeapon.SetAttackStage(AttackStage.Recovery);
        MyWeapon.FinishedAttack();
    }
}
