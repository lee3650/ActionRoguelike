using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public SignalType Type;
    public float Magnitude;
    public Targetable Sender;
    public StatDictionary Stats;
    
    public GameEvent(SignalType myType, float magnitude, Targetable sender, StatDictionary stats)
    {
        Type = myType;
        Magnitude = magnitude;
        Sender = sender;
        Stats = stats;
    }

    public bool HasStat(string stat)
    {
        return Stats.ContainsKey(stat);
    }

    public void MultiplyStat(string key, float amt)
    {
        Stats.MultiplyValue(key, amt);
    }

    public void AddToStat(string key, float amt)
    {
        Stats.AddToValue(key, amt);
    }

    public string GetStat(string key)
    {
        return Stats.GetValue(key);
    }

    public static GameEvent CopyEvent(GameEvent e)
    {
        return new GameEvent(e.Type, e.Magnitude, e.Sender, StatDictionary.CopyStats(e.Stats));
    }

    public void SetStat(string stat, string val)
    {
        Stats.SetValue(stat, val);
    }

    public void RemoveStat(string key)
    {
        Stats.RemoveKey(key);
    }

    public float GetStatAsFloat(string key, float def)
    {
        if (HasStat(key))
        {
            string stat = Stats.GetValue(key);
            return float.Parse(stat);
        }
        SetStat(key, def.ToString());
        return def;
    }

    public static string OddsKey = "odds";
    public static string RecursKey = "recurs";
    public static string SpreadsKey = "spreads";
    public static string RepeatingKey = "repeating"; //if a gameevent contains this key, it will be applied over time 
}
