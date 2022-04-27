using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsList : MonoBehaviour, Initializable
{
    [SerializeField] List<string> initialStats = new List<string>();
    [SerializeField] List<float> initialValues = new List<float>();

    private Dictionary<string, float> StatValues = new Dictionary<string, float>();
    private Dictionary<string, HashSet<StatListener>> StatListeners = new Dictionary<string, HashSet<StatListener>>();

    public void Init()
    {
        StatValues = new Dictionary<string, float>();
        for (int i = 0; i < initialStats.Count; i++)
        {
            StatValues[initialStats[i]] = initialValues[i];
        }
    }

    private void InvokeStatChanged(string stat)
    {
        float newVal = StatValues[stat];
        if (StatListeners.ContainsKey(stat))
        {
            foreach (StatListener sl in StatListeners[stat])
            {
                sl.StatChanged(stat, newVal);
            }
        }
    }
    
    public void RegisterListener(string stat, StatListener listener)
    {
        if (!StatListeners.ContainsKey(stat))
        {
            StatListeners[stat] = new HashSet<StatListener>();
        }
        StatListeners[stat].Add(listener);
    }

    public void UnregisterListener(StatListener listener, string stat)
    {
        if (StatListeners.ContainsKey(stat))
        {
            StatListeners[stat].Remove(listener);
        }
    }

    public void UnregisterListener(StatListener listener)
    {
        foreach (string k in StatListeners.Keys)
        {
            StatListeners[k].Remove(listener);
        }
    }

    public bool HasStat(string stat)
    {
        return StatValues.ContainsKey(stat);
    }

    public void SetStat(string stat, float val)
    {
        StatValues[stat] = val;
        InvokeStatChanged(stat);
    }

    public float GetStat(string stat)
    {
        if (!StatValues.ContainsKey(stat))
        {
            throw new System.Exception("The key " + stat + " was not present in the dictionary for stats list " + name);
        }
        return StatValues[stat];
    }

    public void MultiplyStat(string stat, float amt)
    {
        StatValues[stat] *= amt;
        InvokeStatChanged(stat);
    }

    public void AddToStat(string stat, float amt)
    {
        StatValues[stat] += amt;
        InvokeStatChanged(stat);
    }
}
