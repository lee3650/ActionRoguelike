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
        MyWeapon.OnStartAction += OnStartAction; ;
    }

    private void OnStartAction(string obj)
    {
        //so, later we can just switch this and see what we want to do with it...
        //actually, probably we shouldn't do this. We should
        //Have a bunch of listeners that each listen for a single one and then activate this on a case by case basis, but for now
        //it's fine. 
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
