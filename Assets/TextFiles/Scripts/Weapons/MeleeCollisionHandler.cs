using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionHandler : WeaponCollisionHandler, LateInitializable
{
    [SerializeField] MeleeWeapon MyWeapon;
    [SerializeField] List<GameEvent> MyEventTemplates;

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


    private bool ShouldUseCollision(Entity e)
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
            if (ShouldUseCollision(e))
            {
                foreach (GameEvent ge in MyEventTemplates)
                {
                    ge.Sender = MyWeapon.GetWielder();
                    MyWeapon.LandedHit(col.gameObject);
                    e.HandleEvent(GameEvent.CopyEvent(ge));
                    hitEntities.Add(e);
                    HitEntity(e);
                    lastHit = Time.realtimeSinceStartup;
                }
            }
        } 
    }
}
