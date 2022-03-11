using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;

    public override void EnterState()
    {
        WeaponManager.PauseFaceInput();
        WeaponManager.StartAction(ActionStrings.AttackAction);
    }

    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());

        if (WeaponManager.ActionFinished())
        {
            StateController.EnterState(PlayerMoveState);
        }
    }

    public override void ExitState()
    {
        WeaponManager.PlayFaceInput();
    }
}
