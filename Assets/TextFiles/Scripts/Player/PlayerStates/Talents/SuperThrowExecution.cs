using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowExecution : AbstractSwing, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, Dependency<WeaponManager>, Dependency<MovementController>, Dependency<PlayerInput>, Dependency<PickUpWeapon>
{
    private bool holdingWeapon = true;
    private WeaponManager wm;
    private MovementController MovementController;
    private PlayerInput PlayerInput;
    private PickUpWeapon PickUpWeapon; 
    [SerializeField] float throwKb; 

    public void InjectDependency(HandAndArmGetter hm)
    {
        HandAndArm = hm;
    }

    public void InjectDependency(MovementController mc)
    {
        MovementController = mc; 
    }

    public void InjectDependency(PickUpWeapon puw)
    {
        PickUpWeapon = puw;
    }

    public void InjectDependency(PlayerInput pi)
    {
        PlayerInput = pi; 
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    public void InjectDependency(WeaponManager wm)
    {
        this.wm = wm; 
    }

    public override void EnterState()
    {
        SetupState();
        holdingWeapon = true; 
    }

    public override void UpdateState()
    {
        PartialUpdate();
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
        if (timer > 0.5f * SwingLength && holdingWeapon)
        {
            holdingWeapon = false;
            wm.StartAction(ActionStrings.SuperThrow);
            //don't I have a knockback controller? lol
            MovementController.AddForce(throwKb, -new Vector2(Mathf.Cos(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad), Mathf.Sin(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad)).normalized);
            PickUpWeapon.RemoveSelectedWeapon();
        }
    }

    public override void ExitState()
    {
    }
}
