using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionHandler : WeaponCollisionHandler, LateInitializable
{
    [SerializeField] MeleeWeapon MyWeapon;
    [SerializeField] GameEvent MyEventTemplate;

    private List<Entity> hitEntities = new List<Entity>(); 

    public void LateInit()
    {
        MyWeapon.OnStartAttack += OnStartAttack;
    }

    private void OnStartAttack()
    {
        hitEntities = new List<Entity>();
        hitEntities.Add(MyWeapon.GetWielder());
    }

    public override void HandleCollision(Collider2D col)
    {
        if (col.TryGetComponent<Entity>(out Entity e))
        {
            if (!hitEntities.Contains(e))
            {
                MyEventTemplate.Sender = MyWeapon.GetWielder();
                MyWeapon.LandedHit(col.gameObject);
                e.HandleEvent(GameEvent.CopyEvent(MyEventTemplate));
                hitEntities.Add(e);
            }

        }
    }
}
