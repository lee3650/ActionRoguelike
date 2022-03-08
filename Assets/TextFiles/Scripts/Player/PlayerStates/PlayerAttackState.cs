using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;

    private const string attackAction = "attack";

    public override void EnterState()
    {
        WeaponManager.PauseFaceInput();
        WeaponManager.StartAction(attackAction);
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
