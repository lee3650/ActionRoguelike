using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Vector2 RelativePosition;

    protected Targetable MyWielder;

    protected bool canPickUp = true;

    private AttackStage curStage = AttackStage.Idle;

    public virtual Transform GetTransform()
    {
        return transform; 
    }

    public virtual void SetWielder(Targetable newWielder)
    {
        print("Set wielder: " + newWielder);
        MyWielder = newWielder;
        canPickUp = false;
    }

    public void AllowPickup()
    {
        canPickUp = true; 
    }

    public virtual Vector2 GetRelativePosition()
    {
        return RelativePosition;
    }

    public virtual Targetable GetWielder()
    {
        return MyWielder;
    }

    public bool CanPickUp()
    {
        return canPickUp; 
    }

    public AttackStage GetCurrentStage()
    {
        return curStage;
    }

    public void SetAttackStage(AttackStage stage)
    {
        curStage = stage; 
    }

    public abstract bool ActionAllowed(string action);

    public abstract void Deselect();
    public abstract void Select();

    public abstract void StartAction(string action);
    public abstract bool ActionFinished();
    
}
