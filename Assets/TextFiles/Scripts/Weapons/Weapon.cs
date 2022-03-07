using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //This is temporary. Obviously this isn't a good dependence to have. 
    [SerializeField] HandAndArmGetter HandAndArmGetter;
    [SerializeField] protected Vector2 RelativePosition;

    protected Wielder MyWielder;

    protected bool canPickUp = true; 

    public virtual void SetWielder(Wielder newWielder, Transform hand, Transform arm)
    {
        print("Set wielder: " + newWielder);
        MyWielder = newWielder;
        canPickUp = false;
        HandAndArmGetter.SetHandAndArm(hand, arm);
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

    public abstract void Deselect();
    public abstract void Select();

    public abstract void StartAttack();
    public abstract bool AttackFinished();
    
    public abstract void LandedHit(GameObject hit);
    
}
