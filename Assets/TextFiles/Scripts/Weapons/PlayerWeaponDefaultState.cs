using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDefaultState : State, LateInitializable
{
    [SerializeField] AnticipationState AnticipationState;
    [SerializeField] MeleeWeapon MyWeapon;

    public void LateInit()
    {
        MyWeapon.OnStartAttack += OnStartAttack;
    }

    private void OnStartAttack()
    {
        StateController.EnterState(AnticipationState);
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
