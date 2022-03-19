using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatsList : TalentPolicy
{
    [SerializeField] StatsList StatsList;
    [SerializeField] string stat;
    [SerializeField] float modifier;
    [Tooltip("Multiply or add?")]
    [SerializeField] bool Multiply;

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
}
