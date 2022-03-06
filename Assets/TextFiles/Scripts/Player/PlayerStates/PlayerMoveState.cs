using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] PlayerAttackState PlayerAttackState;
    [SerializeField] PlayerDodgeState PlayerDodgeState;
    [SerializeField] PickUpWeapon PickUpWeapon;

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());

        if (PlayerInput.SelectionDelta() != 0)
        {
            PickUpWeapon.ChangeSelection(PlayerInput.SelectionDelta());
        }

        if (PlayerInput.Attack())
        {
            //um, we'll just go right to the player attack state without going to an attack manager or anything like that, for now
            StateController.EnterState(PlayerAttackState);

        } else if (PlayerInput.Dodge() && PlayerDodgeState.CanDodge())
        {
            StateController.EnterState(PlayerDodgeState);
        }
    }

    public override void ExitState()
    {

    }
}
