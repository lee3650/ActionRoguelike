using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyKnockback : MonoBehaviour, Dependency<TakeKnockback>, Dependency<DirectionSupplier>, Initializable
{
    /// <summary>
    /// Applies knockback to the wielder of this weapon when a hit lands
    /// </summary>
    [SerializeField] float amt;
    [SerializeField] MeleeWeapon MeleeWeapon;
    [SerializeField] MeleeCollisionHandler MeleeCollisionHandler;

    TakeKnockback takeKnockback;
    DirectionSupplier directionSupplier;

    public void Init()
    {
        MeleeWeapon.OnStartAction += OnStartAction;
        MeleeCollisionHandler.HitEntity += HitEntity;
    }

    private void HitEntity(Entity obj)
    {
        KnockWielderBack();
    }

    private void OnStartAction(string obj)
    {
        switch (obj)
        {
            case ActionStrings.AttackAction:
                Active = true;
                break;
            case ActionStrings.ThrowAction:
                Active = false;
                break; 
        }
    }

    public void InjectDependency(TakeKnockback tk)
    {
        takeKnockback = tk; 
    }

    public void InjectDependency(DirectionSupplier ds)
    {
        directionSupplier = ds; 
    }

    public bool Active
    {
        get;
        set;
    }

    public void KnockWielderBack()
    {
        if (Active)
        {
            takeKnockback.ApplyKnockback(amt, directionSupplier.GetDir());
        }
    }
}
