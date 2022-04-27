using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowExecution : AbstractSwing, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, Dependency<WeaponManager>, Dependency<MovementUtility>, Dependency<PickUpWeapon>
{
    private bool holdingWeapon = true;
    private WeaponManager wm;
    private MovementUtility MovementUtility;
    private PickUpWeapon PickUpWeapon;

    private PlayerInput PlayerInput;

    [SerializeField] float throwKb; 

    public void InjectDependency(HandAndArmGetter hm)
    {
        HandAndArm = hm;
    }

    public void InjectDependency(MovementUtility mc)
    {
        MovementUtility = mc;
        PlayerInput = mc.GetPlayerInput();
    }

    public void InjectDependency(PickUpWeapon puw)
    {
        PickUpWeapon = puw;
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
        MovementUtility.MoveTowardInput();
        if (timer > 0.5f * SwingLength && holdingWeapon)
        {
            holdingWeapon = false;
            wm.StartAction(ActionStrings.SuperThrow);

            MovementUtility.AddForce(-new Vector2(Mathf.Cos(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad), Mathf.Sin(PlayerInput.GetDirectionToFace() * Mathf.Deg2Rad)).normalized, throwKb);
            PickUpWeapon.RemoveSelectedWeapon();
        }
    }

    public override void ExitState()
    {
    }
}
