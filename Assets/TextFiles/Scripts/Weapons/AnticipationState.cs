using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnticipationState : AbstractAnticipation, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>
{
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
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {

    }
}
