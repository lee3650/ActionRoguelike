using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollisionHandler : WeaponCollisionHandler, LateInitializable
{
    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] List<GameEvent> MyEventTemplates;

    //We also need to know when we hit any collider so we can stop... um...
    //actually we can just use our trigger, if we have one.
    public event System.Action<Entity> HitEntity = delegate { };

    private List<Entity> hitEntities = new List<Entity>();

    public void LateInit()
    {
        MyWeapon.OnStartAction += OnStartAction;
    }

    private bool ShouldUseCollision(Entity e)
    {
        return !hitEntities.Contains(e);
    }

    public void ResetHitEntities()
    {
        OnStartAction("");
    }

    private void OnStartAction(string action)
    {
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
                    e.HandleEvent(GameEvent.CopyEvent(ge));
                    hitEntities.Add(e);
                    HitEntity(e);
                }
            }
        } 
    }
}
