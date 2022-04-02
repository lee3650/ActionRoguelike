using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPossibleEvent : TalentPolicy, Dependency<AttackModifierList>
{
    [SerializeField] PossibleGameEvent MyEvent;
    
    [SerializeField] private AttackModifierList AttackModifierList;

    public override void UndoPolicy()
    {
        AttackModifierList.RemoveAttackModifier(MyEvent);
        RemoveTalentAndUndoUpgrades();
    }

    public void MultiplyEventChances(float amt)
    {
        MyEvent.Odds *= amt; 
    }

    public void MultiplyEventDamage(float amt)
    {
        MyEvent.Magnitude *= amt; 
    }

    public void AddRecurs(int add)
    {
        MyEvent.Recurs += add; 
    }

    public void AddSpreads(int add)
    {
        MyEvent.Spreads += add;
    }

    public void InjectDependency(AttackModifierList aml)
    {
        if (AttackModifierList == null)
        {
            AttackModifierList = aml; 
        }
    }

    public override void ApplyPolicy()
    {
        AttackModifierList.AddAttackModifier(MyEvent);
    }
}
