using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeleportableWeapon : Teleportable, LateInitializable
{
    [SerializeField] GenericWeapon GenericWeapon;
    [SerializeField] List<string> enabledActions; 

    private bool teleportable = false; 

    public void LateInit()
    {
        GenericWeapon.OnStartAction += OnStartAction;
        GenericWeapon.OnFinishedAction += OnFinishedAction;
    }

    public override bool CanTeleport()
    {
        return teleportable;
    }

    private void OnStartAction(string obj)
    {
        if (enabledActions.Contains(obj))
        {
            teleportable = true; 
        }
        else
        {
            teleportable = false; 
        }
    }

    private void OnFinishedAction()
    {
        teleportable = false; 
    }
}
