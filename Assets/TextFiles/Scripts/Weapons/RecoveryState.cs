using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : AbstractRecovery, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, StatSupplier, Dependency<StatsList>
{
    [SerializeField] MeleeWeapon MyWeapon;
    [SerializeField] bool UsePlayerStats;

    private StatsList PlayerStats;

    public void InjectDependency(ReversedTracker reversedTracker)
    {
        ReversedTracker = reversedTracker;
    }

    public void InjectDependency(HandAndArmGetter handAndArmGetter)
    {
        HandAndArm = handAndArmGetter;
    }

    public override void EnterState()
    {
        SetupState();
        if (UsePlayerStats)
        {
            RecoveryLength = PlayerStats.GetStat(StatsList.RecoveryKey);
        }
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {
        PartialExitState();
        MyWeapon.FinishedAttack();
    }

    public (string, string)[] GetStats()
    {
        return new (string, string)[] { ("Recovery Length", RecoveryLength + "") };
    }

    public void InjectDependency(StatsList dependency)
    {
        PlayerStats = dependency; 
    }
}
