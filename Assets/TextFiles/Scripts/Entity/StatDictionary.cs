using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatDictionary
{
    [SerializeField] private string[] DefaultKeys;
    [SerializeField] private string[] DefaultValues;

    private Dictionary<string, string> Stats = null;

    private bool initialized = false;

    public StatDictionary()
    {

    }

    public StatDictionary(string[] keys, string[] vals)
    {
        SetupStatsList(keys, vals);
        initialized = true; 
    }

    public void RemoveKey(string val)
    {
        Stats.Remove(val);
    }

    void LazyInit()
    {
        if (!initialized)
        {
            MonoBehaviour.print("lazy init with stats " + Stats + ",  default keys " + DefaultKeys.ToString());
            SetupStatsList(DefaultKeys, DefaultValues);
            initialized = true; 
        }
    }

    private void SetupStatsList(string[] keys, string[] vals)
    {
        Stats = new Dictionary<string, string>();

        MonoBehaviour.print("called setup stats with keys " + keys);

        if (keys != null)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Stats[keys[i]] = vals[i];
            }
        }

        DefaultKeys = null;
        DefaultValues = null;
    }

    public void SetValue(string key, string value)
    {
        LazyInit();
        Stats[key] = value; 
    }

    public void AddToValue(string key, float amt)
    {
        if (ContainsKey(key))
        {
            float val = float.Parse(GetValue(key));
            SetValue(key, (amt + val).ToString());
        }
    }

    public void MultiplyValue(string key, float amt)
    {
        if (ContainsKey(key))
        {
            float val = float.Parse(GetValue(key));
            SetValue(key, (amt * val).ToString());
        }
    }

    public bool ContainsKey(string key)
    {
        LazyInit();
        return Stats.ContainsKey(key);
    }

    public string GetValue(string key)
    {
        LazyInit();

        return Stats[key]; 
    }

    public void RemoveStat(string key)
    {
        LazyInit();

        Stats.Remove(key);
    }

    public static StatDictionary CopyStats(StatDictionary original)
    {
        List<string> keys = new List<string>();
        List<string> values = new List<string>();

        if (!original.initialized)
        {
            original.LazyInit();
            //original.SetupStatsList(original.DefaultKeys, original.DefaultValues);
        }

        foreach (string s in original.Stats.Keys)
        {
            keys.Add(s);
            values.Add(original.GetValue(s));
        }

        return new StatDictionary(keys.ToArray(), values.ToArray());
    }
}
