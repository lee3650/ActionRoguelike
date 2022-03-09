using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Vector2 RelativePosition;

    protected Wielder MyWielder;

    protected bool canPickUp = true; 

    public virtual Transform GetTransform()
    {
        return transform; 
    }

    public virtual void SetWielder(Wielder newWielder)
    {
        print("Set wielder: " + newWielder);
        MyWielder = newWielder;
        canPickUp = false;
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

    public abstract bool ActionAllowed(string action);

    public abstract void Deselect();
    public abstract void Select();

    public abstract void StartAction(string action);
    public abstract bool ActionFinished();
    
    public abstract void LandedHit(GameObject hit);
    
}
