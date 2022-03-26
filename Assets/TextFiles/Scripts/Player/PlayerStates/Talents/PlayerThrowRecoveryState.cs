using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowRecoveryState : AbstractRecovery
{
    [SerializeField] MovementUtility MovementUtility;

    public override void EnterState()
    {
        SetupState();
    }

    public override void UpdateState()
    {
        MovementUtility.MoveTowardInput();
        PartialUpdate();
    }

    public override void ExitState()
    {
        PartialExitState();
        MovementUtility.StartRotation();
    }
}
