using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : State
{
    [SerializeField] WeaponManager WeaponManager;
    public override void EnterState()
    {
        WeaponManager.PauseFaceInput();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
