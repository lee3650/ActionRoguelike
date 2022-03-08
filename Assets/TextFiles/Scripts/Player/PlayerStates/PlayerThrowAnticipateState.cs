using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowAnticipateState : AbstractAnticipation
{
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;

    public override void EnterState()
    {
        SetupState();
    }

    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
        PartialUpdate();
    }

    public override void ExitState()
    {

    }
}
