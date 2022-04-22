using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePossibleEvent : TalentPolicy
{
    [SerializeField] AddPossibleEvent AddPossibleEvent;
    [SerializeField] string key;
    [SerializeField] float val;
    [SerializeField] bool multiply; 

    public override void ApplyPolicy()
    {
        if (key == "magnitude")
        {
            AddPossibleEvent.MultiplyEventDamage(val);
            return;
        }

        if (multiply)
        {
            AddPossibleEvent.MultiplyStat(key, val);
        }
        else
        {
            AddPossibleEvent.AddToStat(key, val);
        }
    }

    public override void UndoPolicy()
    {
        if (key == "magnitude")
        {
            AddPossibleEvent.MultiplyEventDamage(1/val);
            RemoveTalentAndUndoUpgrades();
            return;
        }

        if (multiply)
        {
            AddPossibleEvent.MultiplyStat(key, (1 / val));
        }
        else
        {
            AddPossibleEvent.AddToStat(key, -val);
        }
        
        RemoveTalentAndUndoUpgrades();
    }
}
