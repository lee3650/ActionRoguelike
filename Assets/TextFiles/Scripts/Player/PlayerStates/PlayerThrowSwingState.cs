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

    private const string throwAction = "throw";

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
            WeaponManager.StartAction(throwAction);
            MovementController.AddForce(throwKB, -new Vector2(Mathf.Cos(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad), Mathf.Sin(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad)).normalized);
            PickUpWeapon.ChangeSelection(-1);
        }
    }

    public override void ExitState()
    {

    }
}
