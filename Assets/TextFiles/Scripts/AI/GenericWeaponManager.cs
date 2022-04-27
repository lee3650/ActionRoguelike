using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericWeaponManager : AbstractWeaponManager
{
    public override void StartAction(string action)
    {
        CurrentWeapon?.StartAction(action);
    }
}
