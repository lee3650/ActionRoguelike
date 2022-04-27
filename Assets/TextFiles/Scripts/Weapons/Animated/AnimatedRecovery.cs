using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedRecovery : State, Dependency<ReversedTracker>, Dependency<HandAndArmGetter>
{
    private ReversedTracker ReversedTracker;

    [SerializeField] float RecoveryLength;
    [SerializeField] State DefaultState;
    [SerializeField] Animator Animator;

    private HandAndArmGetter HandAndArmGetter;
    public void InjectDependency(HandAndArmGetter handAndArmGetter)
    {
        HandAndArmGetter = handAndArmGetter; 
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    [SerializeField] MeleeWeapon MyWeapon;
    private float timer = 0f; 

    public override void EnterState()
    {
        MyWeapon.SetAttackStage(AttackStage.Recovery);
        print("Entered recovery!");
        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= RecoveryLength)
        {
            StateController.EnterState(DefaultState);
        }
    }

    public override void ExitState()
    {
        MyWeapon.FinishedAttack();
        Animator.enabled = false;
        HandAndArmGetter.ResetPositions(ReversedTracker.Reversed);
        print("Exited recovery!");
    }
}
