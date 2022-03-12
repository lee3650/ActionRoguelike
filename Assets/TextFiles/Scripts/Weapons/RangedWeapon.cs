using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : GenericWeapon, Dependency<HandAndArmGetter>
{
    private HandAndArmGetter HandAndArmGetter; 

    public void InjectDependency(HandAndArmGetter h)
    {
        HandAndArmGetter = h;
    }

    public override void Select()
    {
        base.Select();
        HandAndArmGetter.SetArmRotation(90f);
        transform.localEulerAngles = new Vector3(0, 0, -90);
    }

    public override void Deselect()
    {
        base.Deselect();
        HandAndArmGetter.SetArmRotation(0f);
    }
}
