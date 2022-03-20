using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowRecovery : AbstractRecovery, Dependency<LastThrownWeaponManager>, Dependency<HandAndArmGetter>, 
    Dependency<ReversedTracker>, Dependency<PlayerMoveState>, Dependency<MovementController>, Dependency<PlayerInput>
{
    private LastThrownWeaponManager thrownWeapon; 
    private PlayerMoveState PlayerMoveState;

    private MovementController MovementController;
    private PlayerInput input;

    private Weapon lastWeapon;

    public void InjectDependency(LastThrownWeaponManager lt)
    {
        thrownWeapon = lt; 
    }

    public void InjectDependency(MovementController lt)
    {
        MovementController = lt;
    }

    public void InjectDependency(PlayerInput lt)
    {
        input = lt;
    }

    public void InjectDependency(HandAndArmGetter lt)
    {
        HandAndArm = lt;
    }

    public void InjectDependency(ReversedTracker lt)
    {
        ReversedTracker = lt;
    }

    public void InjectDependency(PlayerMoveState lt)
    {
        PlayerMoveState = lt;
    }

    public override void EnterState()
    {
        SetupState();
        lastWeapon = thrownWeapon.GetLastThrownWeapon();
        thrownWeapon.PickedUpLastWeapon += PickedUpLastWeapon;
    }

    private void PickedUpLastWeapon()
    {
        lastWeapon.GetComponent<SuperThrowState>().PickedUp();
        StateController.EnterState(PlayerMoveState);
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        MovementController.MoveInDirection(input.GetDirectionalInput());

        if (timer < RecoveryLength)
        {
            SwingAnimator.AnimateRecovery(HandAndArm, startRotation, startHandRotation, end, RecoveryLength, timer, dir, w_dir);
        }
    }

    public override void ExitState()
    {
        thrownWeapon.PickedUpLastWeapon -= PickedUpLastWeapon; 
        PartialExitState();
    }
}
