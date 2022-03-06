using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Wielder MyWielder;

    protected bool canPickUp = true; 

    public virtual void SetWielder(Wielder newWielder)
    {
        print("Set wielder: " + newWielder);
        MyWielder = newWielder;
        canPickUp = false; 
    }

    public virtual Targetable GetWielder()
    {
        return MyWielder;
    }

    public bool CanPickUp()
    {
        return canPickUp; 
    }

    public abstract void Deselect();
    public abstract void Select();

    public abstract void StartAttack();
    public abstract bool AttackFinished();
    
    public abstract void LandedHit(GameObject hit);
    
}
