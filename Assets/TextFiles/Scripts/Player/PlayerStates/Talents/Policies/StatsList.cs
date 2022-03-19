using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsList : MonoBehaviour
{
    [SerializeField] List<string> Stats;
    [SerializeField] List<float> Values; 

    public bool HasStat(string stat)
    {
        return Stats.Contains(stat);
    }

    public float GetStat(string stat)
    {
        return Values[Stats.IndexOf(stat)];
    }

    public void MultiplyStat(string stat, float amt)
    {
        Values[Stats.IndexOf(stat)] *= amt; 
    }

    public void AddToStat(string stat, float amt)
    {
        Values[Stats.IndexOf(stat)] += amt;
    }
}
