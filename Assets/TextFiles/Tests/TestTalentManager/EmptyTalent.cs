using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTalent : State, Talent
{
    public bool useable = true; 

    public bool CanUseTalent()
    {
        return useable;
    }

    public override void EnterState()
    {

    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }
}
