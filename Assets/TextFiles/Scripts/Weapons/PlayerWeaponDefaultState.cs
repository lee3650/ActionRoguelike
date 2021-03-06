using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDefaultState : State, LateInitializable
{
    [SerializeField] List<string> Actions;
    [SerializeField] State[] Reactions;
    [SerializeField] GenericWeapon MyWeapon;

    public void LateInit()
    {
        MyWeapon.OnStartAction += OnStartAction; ;
    }

    private void OnStartAction(string action)
    {
        int index = Actions.IndexOf(action);
        if (index >= 0)
        {
            if (Reactions[index] != null)
            {
                StateController.EnterState(Reactions[index]);
            }
        }
    }

    public override void EnterState()
    {
        MyWeapon.SetAttackStage(AttackStage.Idle);
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
