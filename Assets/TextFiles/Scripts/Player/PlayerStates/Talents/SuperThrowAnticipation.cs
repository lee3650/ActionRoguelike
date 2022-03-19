using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowAnticipation : AbstractAnticipation, Talent, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, Dependency<ManaManager>
{
    public void InjectDependency(HandAndArmGetter hm)
    {
        HandAndArm = hm;
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    private ManaManager manaManager;

    public bool CanUseTalent()
    {
        return manaManager.ChargesRemaining(1);
    }

    public void InjectDependency(ManaManager mm)
    {
        manaManager = mm;
    }

    public override void EnterState()
    {
        SetupState();
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {

    }
}
