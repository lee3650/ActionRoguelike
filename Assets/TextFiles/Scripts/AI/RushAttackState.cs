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
    [SerializeField] List<AttackStage> ForwardStage = new List<AttackStage>() { AttackStage.Execution };
    [SerializeField] List<AttackStage> BackwardStage = new List<AttackStage>() { AttackStage.Anticipation };
    [SerializeField] FaceTarget FaceTarget;
    [SerializeField] bool RotateWhileAttacking; 

    public override void EnterState()
    {
        WeaponManager.StartAction(ActionStrings.AttackAction);
    }

    public override void UpdateState()
    {
        if (BackwardStage.Contains(WeaponManager.GetCurrentStage()))
        {
            MovementController.AddForce(backupSpeed, -MyDirection.GetDir());
        }

        if (ForwardStage.Contains(WeaponManager.GetCurrentStage()))
        {
            MovementController.AddForce(rushSpeed, MyDirection.GetDir());
        }

        if (WeaponManager.ActionFinished())
        {
            StateController.EnterState(NextState);
        }

        if (RotateWhileAttacking)
        {
            FaceTarget.LookAtTarget();
        }
    }

    public override void ExitState()
    {

    }
}
