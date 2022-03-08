using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowSwingState : AbstractSwing
{
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
