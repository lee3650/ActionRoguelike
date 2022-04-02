using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatsList : TalentPolicy, Dependency<StatsList>
{
    [SerializeField] bool InjectStats;
    [SerializeField] StatsList StatsList;
    [SerializeField] string stat;
    [SerializeField] float modifier;
    [Tooltip("Multiply or add?")]
    [SerializeField] bool Multiply;

    public void InjectDependency(StatsList st)
    {
        if (InjectStats)
        {
            StatsList = st; 
        }
    }

    public override void ApplyPolicy()
    {
        if (Multiply)
        {
            StatsList.MultiplyStat(stat, modifier);
        } else
        {
            StatsList.AddToStat(stat, modifier);
        }
    }

    public override void UndoPolicy()
    {
        if (Multiply)
        {
            StatsList.MultiplyStat(stat, 1 / modifier);
        } else
        {
            StatsList.AddToStat(stat, -modifier);
        }
        RemoveTalentAndUndoUpgrades();
    }
}
