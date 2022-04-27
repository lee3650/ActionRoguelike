using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOnHit : MonoBehaviour, LateInitializable, Dependency<ManaManager>, Enableable, StatSupplier
{
    [SerializeField] GenericCollisionHandler CollisionHandler;
    [SerializeField] ManaManager manaManager;
    [SerializeField] float ManaAmt;

    private bool active = true; 

    public void SetEnabled(bool e)
    {
        active = e; 
    }

    public void InjectDependency(ManaManager manaManager)
    {
        this.manaManager = manaManager; 
    }

    public void LateInit()
    {
        CollisionHandler.HitEntity += HitEntity;
    }

    private void HitEntity(Entity obj)
    {
        if (active && manaManager != null)
        {
            manaManager.AddMana(ManaAmt);
        }
    }

    public (string, string)[] GetStats()
    {
        return new (string, string)[] { ("Mana From Hit", ManaAmt + "") };
    }
}
