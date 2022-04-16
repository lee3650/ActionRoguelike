using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedRecovery : State, Dependency<ReversedTracker>
{
    private ReversedTracker ReversedTracker;

    [SerializeField] float RecoveryLength;
    [SerializeField] State DefaultState;

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    [SerializeField] MeleeWeapon MyWeapon;
    private float timer = 0f; 

    public override void EnterState()
    {
        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= RecoveryLength)
        {
            StateController.EnterState(DefaultState);
        }
    }

    public override void ExitState()
    {
        ReversedTracker.ToggleReversed();
        MyWeapon.FinishedAttack();
    }
}
