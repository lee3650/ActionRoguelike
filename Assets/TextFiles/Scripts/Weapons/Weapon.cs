using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Wielder MyWielder;

    public virtual void SetWielder(Wielder newWielder)
    {
        MyWielder = newWielder;
    }

    public virtual Targetable GetWielder()
    {
        return MyWielder;
    }

    public abstract void StartAttack();
    public abstract bool AttackFinished();
    
    public abstract void LandedHit(GameObject hit);
    
}
