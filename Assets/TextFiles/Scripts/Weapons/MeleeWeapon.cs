using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class MeleeWeapon : Weapon
{
    public event Action OnStartAttack = delegate { };

    private bool finishedAttack = true;

    [SerializeField] SpriteRenderer[] Images; 

    public override void Select()
    {
        foreach (SpriteRenderer sr in Images)
        {
            sr.enabled = true; 
        }
    }

    public override void Deselect()
    {
        foreach(SpriteRenderer sr in Images)
        {
            sr.enabled = false;
        }
    }

    public void FinishedAttack()
    {
        finishedAttack = true;
    }

    public override void LandedHit(GameObject hit)
    {
        MyWielder.OnHitLands(hit);
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
