using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecallState : State, Talent, Dependency<MovementController>, Dependency<PlayerInput>, Dependency<LastThrownWeaponManager>, Dependency<ManaManager>, Dependency<PlayerMoveState>
{
    [SerializeField] float recallSpeed = 300f; 

    private LastThrownWeaponManager lastThrownWeapon; 
    private MovementController MovementController;
    private PlayerInput PlayerInput;
    private ManaManager ManaManager;

    private PlayerMoveState PlayerMoveState;

    private Weapon thrownWeapon;

    private Rigidbody2D lastWeapon;

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

        //later we'll do thrownWeapon.StartAction("recall"); or something 
        //I do want arguments for that on some level though. 
        thrownWeapon = lastThrownWeapon.GetLastThrownWeapon();
        thrownWeapon.AllowPickup();
        thrownWeapon.GetComponent<PickupLength>().LengthOfPickup = 0f;
        lastWeapon = thrownWeapon.GetComponent<Rigidbody2D>();
        lastWeapon.isKinematic = false;

        if (damageEnabled)
        {
            lastWeapon.GetComponent<GenericCollisionHandler>().ResetHitEntities();
            lastWeapon.GetComponent<SendCollision>().StartColliding();
        }
    }

    private void PickedUpLastWeapon()
    {
        StateController.EnterState(PlayerMoveState);
    }

    public override void UpdateState()
    {
        //I'm going to do this in a really crappy way for now. 
        lastWeapon.AddForce(((Vector2)transform.position - lastWeapon.position) * recallSpeed);

        print("in recall state!");

        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
    }

    public override void ExitState()
    {
        print("leaving recall state!");

        lastThrownWeapon.PickedUpLastWeapon += PickedUpLastWeapon;

        if (lastWeapon != null)
        {
            lastWeapon.isKinematic = true;
            lastWeapon.GetComponent<SendCollision>().StopColliding();
        }
    }
}
