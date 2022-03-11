using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] AbstractWeaponManager WeaponManager;
    [SerializeField] State NextState;

    public override void EnterState()
    {
        WeaponManager.StartAction(ActionStrings.AttackAction);
    }

    public override void UpdateState()
    {
        if (WeaponManager.ActionFinished())
        {
            StateController.EnterState(NextState);
        }
    }
    
    public override void ExitState()
    {

    }
}
