using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowSwingState : AbstractSwing
{
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PickUpWeapon PickUpWeapon;
    [SerializeField] float throwKB; 

    private bool holdingWeapon = true;

    public override void EnterState()
    {
        SetupState();
        holdingWeapon = true; 
    }

    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
        PartialUpdate();
        if (timer > 0.5f * SwingLength && holdingWeapon)
        {
            holdingWeapon = false;
            WeaponManager.StartAction(ActionStrings.ThrowAction);
            MovementController.AddForce(throwKB, -new Vector2(Mathf.Cos(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad), Mathf.Sin(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad)).normalized);
            PickUpWeapon.RemoveSelectedWeapon();
        }
    }

    public override void ExitState()
    {

    }
}
