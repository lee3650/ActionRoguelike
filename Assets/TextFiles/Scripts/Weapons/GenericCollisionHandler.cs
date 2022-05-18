using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollisionHandler : WeaponCollisionHandler, LateInitializable, Dependency<AttackModifierList>, StatSupplier, Dependency<StatsList>, StatListener 
{
    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] WielderSupplier WielderSupplier;
    [SerializeField] List<GameEvent> MyEventTemplates;
    [SerializeField] bool UsePlayerDamageStat = false;
    
    private AttackModifierList AttackModifiers; 

    //We also need to know when we hit any collider so we can stop... um...
    //actually we can just use our trigger, if we have one.
    public event System.Action<Entity> HitEntity = delegate { };

    private List<Entity> hitEntities = new List<Entity>();

    private StatsList PlayerStats;

    private GameEvent baseEvent = null;

    public void InjectDependency(StatsList dependency)
    {
        if (UsePlayerDamageStat)
        {
            PlayerStats = dependency;
            PlayerStats.RegisterListener(StatsList.BaseDamageKey, this);
            baseEvent = new GameEvent(SignalType.Physical, PlayerStats.GetStat(StatsList.BaseDamageKey), WielderSupplier.GetWielder(),
                new StatDictionary(new string[] { }, new string[] { }));
            MyEventTemplates.Insert(0, baseEvent);
        }
    }

    public void StatChanged(string stat, float newVal)
    {
        baseEvent.Magnitude = newVal; 
    }

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
        hitEntities.Add(WielderSupplier.GetWielder());
    }

    public override void HandleCollision(Collider2D col)
    {
        if (col.TryGetComponent<Entity>(out Entity e))
        {
            if (ShouldUseCollision(e))
            {
                List<GameEvent> effectiveEvents = new List<GameEvent>();
                effectiveEvents.AddRange(MyEventTemplates);

                Prereq.Assert(AttackModifiers != null, "Attack modifiers was not injected!");
                
                effectiveEvents.AddRange(AttackModifiers.GetAttackModifiers());

                foreach (GameEvent ge in effectiveEvents)
                {
                    ge.Sender = WielderSupplier.GetWielder();
                    e.HandleEvent(GameEvent.CopyEvent(ge));
                    hitEntities.Add(e);
                    HitEntity(e);
                }
            }
        } 
    }

    //So, yeah this is a bit of a mixing of responsibilities but not doing this would come at the price of encapsulation 
    private float GetTotalDamage()
    {
        float result = 0;
        foreach (GameEvent e in MyEventTemplates)
        {
            if (e.Type != SignalType.Knockback)
            {
                result += e.Magnitude;
            }
        }
        return result; 
    }
    
    public (string, string)[] GetStats()
    {
        return new (string, string)[] { ("Damage", GetTotalDamage() + "") };
    }
}