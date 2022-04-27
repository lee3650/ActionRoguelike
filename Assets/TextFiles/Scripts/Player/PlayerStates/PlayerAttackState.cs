using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] MovementUtility MovementUtility;

    public override void EnterState()
    {
        MovementUtility.StopRotation();
        //WeaponManager.PauseFaceInput();
        WeaponManager.StartAction(ActionStrings.AttackAction);
    }

    public override void UpdateState()
    {
        //MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());

        MovementUtility.MoveTowardInput();

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
