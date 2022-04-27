using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : State
{
    [SerializeField] InventoryShower InventoryShower;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] PlayerMoveState NextState;

    public override void EnterState()
    {
        InventoryShower.ShowInventory();
    }
    
    public override void UpdateState()
    {
        if (PlayerInput.ToggleInventory())
        {
            InventoryShower.HideInventory();
            StateController.EnterState(NextState);
        }
    }
    
    public override void ExitState()
    {
    
    }
}
