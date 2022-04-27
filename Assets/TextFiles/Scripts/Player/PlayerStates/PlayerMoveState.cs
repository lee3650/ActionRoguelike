using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] PlayerAttackState PlayerAttackState;
    [SerializeField] PlayerDodgeState PlayerDodgeState;
    [SerializeField] PickUpWeapon PickUpWeapon;
    [SerializeField] ActiveTalentManager TalentManager;
    [SerializeField] PlayerPickupState PlayerPickupState;
    [SerializeField] MovementUtility MovementUtility;
    [SerializeField] InventoryState InventoryState;

    public override void EnterState()
    {
        MovementUtility.StartRotation();
    }

    public override void UpdateState()
    {
        MovementUtility.MoveTowardInput();

        if (PlayerInput.SelectionDelta() != 0)
        {
            PickUpWeapon.ChangeSelection(PlayerInput.SelectionDelta());
        }

        if (PlayerInput.ToggleInventory())
        {
            StateController.EnterState(InventoryState);
            return; 
        }

        int tal = PlayerInput.GetTalentToActivate();

        if (TalentManager.IsTalentAllowed(tal))
        {
            print("active talent valid!");
            State newState = TalentManager.GetActiveTalent(tal);
            StateController.EnterState(newState);
            //not sure if the return is necessary
            return; 
        }

        if (PlayerInput.Attack())
        {
            //um, we'll just go right to the player attack state without going to an attack manager or anything like that, for now
            StateController.EnterState(PlayerAttackState);

        } else if (PlayerInput.Dodge() && PlayerDodgeState.CanDodge())
        {
            StateController.EnterState(PlayerDodgeState);
        }

        if (PlayerInput.PickUpItems())
        {
            StateController.EnterState(PlayerPickupState);
            return; 
        }
    }

    public override void ExitState()
    {

    }
}
