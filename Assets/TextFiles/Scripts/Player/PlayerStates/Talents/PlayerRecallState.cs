using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecallState : State, Talent, Dependency<MovementController>, Dependency<PlayerInput>, Dependency<LastThrownWeaponManager>, Dependency<ManaManager>, Dependency<PlayerMoveState>
{
    private LastThrownWeaponManager lastThrownWeapon; 
    private MovementController MovementController;
    private PlayerInput PlayerInput;
    private ManaManager ManaManager;

    private PlayerMoveState PlayerMoveState;

    private Weapon thrownWeapon;

    private bool damageEnabled = false; 

    public void EnableDamage()
    {
        damageEnabled = true; 
    }

    public bool CanUseTalent()
    {
        return ManaManager.ChargesRemaining(1);
    }
    public void InjectDependency(PlayerMoveState p)
    {
        PlayerMoveState = p; 
    }

    public void InjectDependency(LastThrownWeaponManager ltw)
    {
        lastThrownWeapon = ltw;
    }

    public void InjectDependency(MovementController mc)
    {
        MovementController = mc; 
    }
    
    public void InjectDependency(PlayerInput pi)
    {
        PlayerInput = pi; 
    }

    public void InjectDependency(ManaManager mm)
    {
        ManaManager = mm; 
    }

    public override void EnterState()
    {
        if (!lastThrownWeapon.HasThrownWeapon())
        {
            StateController.EnterState(PlayerMoveState);
            return;
        }

        lastThrownWeapon.PickedUpLastWeapon += PickedUpLastWeapon;

        ManaManager.UseCharge();

        thrownWeapon = lastThrownWeapon.GetLastThrownWeapon();

        thrownWeapon.StartAction(ActionStrings.RecallAction);

        if (damageEnabled)
        {
            thrownWeapon.GetComponent<SendCollision>().StartColliding();
        }
    }

    private void PickedUpLastWeapon()
    {
        thrownWeapon.GetComponent<WeaponRecallState>().OnPickup();
        StateController.EnterState(PlayerMoveState);
    }

    public override void UpdateState()
    {
        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
    }

    public override void ExitState()
    {
        print("leaving recall state!");
        lastThrownWeapon.PickedUpLastWeapon -= PickedUpLastWeapon;
    }
}
