using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPossibleEvent : TalentPolicy, Dependency<AttackModifierList>
{
    [SerializeField] GameEvent MyEvent;
    
    [SerializeField] private AttackModifierList AttackModifierList;

    public override void UndoPolicy()
    {
        AttackModifierList.RemoveAttackModifier(MyEvent);
        RemoveTalentAndUndoUpgrades();
    }

    public void MultiplyStat(string stat, float val)
    {
        MyEvent.MultiplyStat(stat, val);
    }

    public void AddToStat(string key, float val)
    {
        MyEvent.AddToStat(key, val);
    }

    public void MultiplyEventChances(float amt)
    {
        MyEvent.MultiplyStat(GameEvent.OddsKey, amt); 
    }

    public void MultiplyEventDamage(float amt)
    {
        MyEvent.Magnitude *= amt; 
    }

    public void AddRecurs(int add)
    {
        MyEvent.AddToStat(GameEvent.RecursKey, add); 
    }

    public void AddSpreads(int add)
    {
        MyEvent.AddToStat(GameEvent.SpreadsKey, add);
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
