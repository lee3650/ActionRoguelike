using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnticipationState : AbstractAnticipation, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, StatSupplier, Dependency<StatsList>
{
    [SerializeField] Weapon MyWeapon;
    [SerializeField] bool UseStatsList = false;

    private StatsList statsList;

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
        MyWeapon.SetAttackStage(AttackStage.Anticipation);
        if (UseStatsList)
        {
            AnticipationLength = statsList.GetStat(StatsList.AnticipationKey);
        }
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {

    }

    public (string, string)[] GetStats()
    {
        return new (string, string)[] { ("Anticipation Length", AnticipationLength + "") };
    }

    public void InjectDependency(StatsList dependency)
    {
        statsList = dependency;
    }
}
