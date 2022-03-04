using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    [SerializeField] MeleeWeapon MeleeWeapon;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;

    public override void EnterState()
    {
        MeleeWeapon.StartAttack();
    }
    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());

        if (MeleeWeapon.AttackFinished())
        {
            StateController.EnterState(PlayerMoveState);
        }
    }
    public override void ExitState()
    {

    }
}
