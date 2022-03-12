using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class GenericWeapon : Weapon
{
    public event Action<string> OnStartAction = delegate { };

    private bool finishedAction = true;

    [SerializeField] SpriteRenderer[] Images;

    [SerializeField] Collider2D col;
    //this is definitely duplication. 
    [SerializeField] List<string> HandledActions;

    public override void Select()
    {
        foreach (SpriteRenderer sr in Images)
        {
            sr.enabled = true;
        }
        col.enabled = true;
    }

    public override bool ActionAllowed(string action)
    {
        //might want a blacklist as well
        return HandledActions.Contains(action);
    }

    public override void Deselect()
    {
        foreach (SpriteRenderer sr in Images)
        {
            sr.enabled = false;
        }
        col.enabled = false;
    }

    public void FinishedAttack()
    {
        finishedAction = true;
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
