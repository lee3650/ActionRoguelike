using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponManager : AbstractWeaponManager
{
    public override void StartAction(string s)
    {

    }

    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon;
    }
}
