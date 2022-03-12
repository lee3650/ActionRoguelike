using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCollisionHandler : WeaponCollisionHandler
{
    [SerializeField] Weapon MyWeapon;
    [SerializeField] GameEvent[] EventTemplates; 

    public override void HandleCollision(Collider2D col)
    {
        if (col.TryGetComponent<Entity>(out Entity e))
        {
            foreach (GameEvent et in EventTemplates)
            {
                et.Sender = MyWeapon.GetWielder();
                e.HandleEvent(GameEvent.CopyEvent(et));
            }
        }
    }
}
