using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollisionHandler : WeaponCollisionHandler, LateInitializable, Dependency<AttackModifierList>
{
    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] List<GameEvent> MyEventTemplates;

    private AttackModifierList AttackModifiers; 

    //We also need to know when we hit any collider so we can stop... um...
    //actually we can just use our trigger, if we have one.
    public event System.Action<Entity> HitEntity = delegate { };

    private List<Entity> hitEntities = new List<Entity>();

    public void InjectDependency(AttackModifierList aml)
    {
        print("dependency injected! " + aml);
        AttackModifiers = aml; 
    }

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
                List<GameEvent> effectiveEvents = new List<GameEvent>();
                effectiveEvents.AddRange(MyEventTemplates);
                if (AttackModifiers == null)
                {
                    throw new System.Exception("Attack modifiers was not injected!");
                }
                effectiveEvents.AddRange(AttackModifiers.GetAttackModifiers());

                foreach (GameEvent ge in effectiveEvents)
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
