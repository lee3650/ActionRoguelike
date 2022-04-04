using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsItem : Item, LateInitializable
{
    [SerializeField] string description; 

    public void LateInit()
    {
        StatSupplier[] stats = GetComponents<StatSupplier>();

        if (stats.Length > 0)
        {
            description += "\n\n";
        }

        foreach (StatSupplier s in stats)
        {
            for (int i = 0; i < s.GetStats().Length; i++)
            {
                description += string.Format("{0}: {1}\n", s.GetStats()[i].Item1, s.GetStats()[i].Item2);
            }
        }
    }

    public override string GetDescription()
    {
        return description;
    }
}
