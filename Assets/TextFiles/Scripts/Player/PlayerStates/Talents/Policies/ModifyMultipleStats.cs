using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyMultipleStats : TalentPolicy, Dependency<StatsList>
{
    [SerializeField] bool InjectStats;
    [SerializeField] StatsList StatsList;
    [SerializeField] string[] stats;
    [SerializeField] float[] modifiers;
    [Tooltip("Multiply or add?")]
    [SerializeField] bool[] Multiply;

    public void InjectDependency(StatsList st)
    {
        if (InjectStats)
        {
            StatsList = st;
        }
    }

    public override void ApplyPolicy()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (Multiply[i])
            {
                StatsList.MultiplyStat(stats[i], modifiers[i]);
            }
            else
            {
                StatsList.AddToStat(stats[i], modifiers[i]);
            }
        }
    }

    public override void UndoPolicy()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (Multiply[i])
            {
                StatsList.MultiplyStat(stats[i], 1 / modifiers[i]);
            }
            else
            {
                StatsList.AddToStat(stats[i], -modifiers[i]);
            }
        }

        RemoveTalentAndUndoUpgrades();
    }
}
