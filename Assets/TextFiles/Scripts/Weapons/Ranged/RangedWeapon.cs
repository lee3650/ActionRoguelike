using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class RangedWeapon : GenericWeapon, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>
{
    private HandAndArmGetter HandAndArmGetter;
    private ReversedTracker ReversedTracker;


    public void InjectDependency(HandAndArmGetter h)
    {
        HandAndArmGetter = h;
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    public override void Select()
    {
        base.Select();
        HandAndArmGetter.SetArmRotation(90f);
        HandAndArmGetter.SetHandRotation(0);
        transform.localEulerAngles = new Vector3(0, 0, -90);
    }

    public override void Deselect()
    {
        base.Deselect();
        HandAndArmGetter.SetArmRotation(SwingAnimator.GetStart(ReversedTracker.Reversed));
        HandAndArmGetter.SetHandRotation(SwingAnimator.GetStart(ReversedTracker.Reversed));
    }
}
