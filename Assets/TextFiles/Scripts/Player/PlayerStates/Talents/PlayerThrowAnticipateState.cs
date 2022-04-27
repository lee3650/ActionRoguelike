using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowAnticipateState : AbstractAnticipation, Talent
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] MovementUtility MovementUtility;

    public bool CanUseTalent()
    {
        return WeaponManager.DoesCurrentWeaponAllowAction(ActionStrings.ThrowAction);
    }

    public override void EnterState()
    {
        WeaponManager.PauseFaceInput();
        SetupState();
    }

    public override void UpdateState()
    {
        MovementUtility.MoveTowardInput();
        PartialUpdate();
    }

    public override void ExitState()
    {

    }
}
