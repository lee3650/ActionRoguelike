using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowExecution : AbstractSwing, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, Dependency<WeaponManager>
{
    private WeaponManager wm; 

    public void InjectDependency(HandAndArmGetter hm)
    {
        HandAndArm = hm;
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    public void InjectDependency(WeaponManager wm)
    {
        this.wm = wm; 
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
        throw new System.NotImplementedException();
    }
}
