using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class MeleeWeapon : Weapon
{
    public event Action OnStartAttack = delegate { };

    private bool finishedAttack = false;

    public void FinishedAttack()
    {
        finishedAttack = true;
    }

    public override void StartAttack()
    {
        finishedAttack = false;
        OnStartAttack();
    }

    public override bool AttackFinished()
    {
        return finishedAttack; 
    }
}
