using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupState : State
{
    [SerializeField] PlayerMoveState NextState;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] AddToInventory AddToInventory;
    [SerializeField] HandAndArmGetter HandAndArmGetter;

    private float timer;
    private float length; 

    public override void EnterState()
    {
        AddToInventory.FindQueuedWeapon();
        if (!AddToInventory.HasQueuedWeapon())
        {
            StateController.EnterState(NextState);
            return; 
        }

        length = AddToInventory.GetPickupDelay();

        HandAndArmGetter.SetHandPosition(AddToInventory.QueuedWeapon.transform.position);

        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;
        if (!PlayerInput.PickUpItems())
        {
            StateController.EnterState(NextState);
        } 
        if (timer >= length)
        {
            AddToInventory.PickupQueued();
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {
        HandAndArmGetter.ResetHandPosition();
    }
}
