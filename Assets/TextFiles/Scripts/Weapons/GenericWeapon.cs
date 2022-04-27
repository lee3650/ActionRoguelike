using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class GenericWeapon : Weapon
{
    public event Action<string> OnStartAction = delegate { };
    public event Action OnFinishedAction = delegate { };

    private bool finishedAction = true;

    [SerializeField] SpriteRenderer[] Images;

    [SerializeField] Collider2D col;
    //this is definitely duplication. 
    [SerializeField] List<string> HandledActions;

    public event Action<bool> SelectionChanged = delegate { };

    public override void Select()
    {
        foreach (SpriteRenderer sr in Images)
        {
            sr.enabled = true;
        }
        col.enabled = true;
        SelectionChanged(true);
    }

    public override bool ActionAllowed(string action)
    {
        //might want a blacklist as well
        return HandledActions.Contains(action);
    }

    public override void Deselect()
    {
        print("deselected weapon!");
        foreach (SpriteRenderer sr in Images)
        {
            sr.enabled = false;
        }
        col.enabled = false;

        SelectionChanged(false);
    }

    public void FinishedAttack()
    {
        finishedAction = true;
        OnFinishedAction();
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
