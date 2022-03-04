using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDefaultState : State, LateInitializable
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] SwingState SwingState;
    [SerializeField] MeleeWeapon MyWeapon;

    public void LateInit()
    {
        MyWeapon.OnStartAttack += OnStartAttack;
    }

    private void OnStartAttack()
    {
        StateController.EnterState(SwingState);
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        FacePlayerInput.FaceInput();
    }
    public override void ExitState()
    {

    }
}
