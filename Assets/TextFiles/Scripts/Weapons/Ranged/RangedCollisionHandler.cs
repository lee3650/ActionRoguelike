using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCollisionHandler : WeaponCollisionHandler, Dependency<AttackModifierList>
{
    [SerializeField] Weapon MyWeapon;
    [SerializeField] GameEvent[] EventTemplates;

    private AttackModifierList AttackModifierList;
    public void InjectDependency(AttackModifierList aml)
    {
        AttackModifierList = aml; 
    }

    public override void HandleCollision(Collider2D col)
    {
        if (col.TryGetComponent<Entity>(out Entity e))
        {
            List<GameEvent> effectiveEvents = new List<GameEvent>();
            effectiveEvents.AddRange(EventTemplates);
            if (EventTemplates == null)
            {
                throw new System.Exception("Attack modifiers was not injected!");
            }

            effectiveEvents.AddRange(AttackModifierList.GetAttackModifiers());

            foreach (GameEvent ge in effectiveEvents)
            {
                ge.Sender = MyWeapon.GetWielder();
                e.HandleEvent(GameEvent.CopyEvent(ge));
            }
        }
    }
}
