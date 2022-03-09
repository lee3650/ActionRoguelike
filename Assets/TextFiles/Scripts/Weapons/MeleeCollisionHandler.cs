using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionHandler : WeaponCollisionHandler, LateInitializable
{
    [SerializeField] MeleeWeapon MyWeapon;
    [SerializeField] GameEvent MyEventTemplate;

    //We also need to know when we hit any collider so we can stop... um...
    //actually we can just use our trigger, if we have one.
    public event System.Action<Entity> HitEntity = delegate { };

    private List<Entity> hitEntities = new List<Entity>();

    private float lastHit;

    public void LateInit()
    {
        MyWeapon.OnStartAction += OnStartAction;
        lastHit = 0f; 
    }


    private bool ShouldIgnoreCollision(Entity e)
    {
        return !hitEntities.Contains(e);
    }

    private void OnStartAction(string action)
    {
        //later - switch action or have one handler per action 

        hitEntities = new List<Entity>();
        hitEntities.Add(MyWeapon.GetWielder());
    }

    public override void HandleCollision(Collider2D col)
    {
        if (col.TryGetComponent<Entity>(out Entity e))
        {
            if (ShouldIgnoreCollision(e))
            {
                MyEventTemplate.Sender = MyWeapon.GetWielder();
                MyWeapon.LandedHit(col.gameObject);
                e.HandleEvent(GameEvent.CopyEvent(MyEventTemplate));
                hitEntities.Add(e);
                HitEntity(e);
                lastHit = Time.realtimeSinceStartup; 
            }
        } 
    }
}
