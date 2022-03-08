using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class MeleeWeapon : Weapon
{
    public event Action<string> OnStartAction = delegate { };

    private bool finishedAction = true;

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
        finishedAction = true;
    }

    public override void LandedHit(GameObject hit)
    {
        MyWielder.OnHitLands(hit);
    }

    public override void StartAction(string action)
    {
        finishedAction = false;
        OnStartAction(action);
    }

    public override bool ActionFinished()
    {
        return finishedAction; 
    }

}
