using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttackState : State
{
    [SerializeField] MovementController MovementController;
    [SerializeField] AbstractWeaponManager WeaponManager;
    [SerializeField] DirectionSupplier MyDirection;
    [SerializeField] State NextState;
    [SerializeField] float rushSpeed;
    [SerializeField] float backupSpeed;

    public override void EnterState()
    {
        WeaponManager.StartAction(ActionStrings.AttackAction);
    }

    public override void UpdateState()
    {
        if (WeaponManager.GetCurrentStage() == AttackStage.Anticipation)
        {
            MovementController.AddForce(backupSpeed, -MyDirection.GetDir());
        }

        if (WeaponManager.GetCurrentStage() == AttackStage.Execution)
        {
            MovementController.AddForce(rushSpeed, MyDirection.GetDir());
        }

        if (WeaponManager.ActionFinished())
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {

    }
}
