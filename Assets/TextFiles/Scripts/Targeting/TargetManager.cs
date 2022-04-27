using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class TargetManager : MonoBehaviour, Initializable
{
    private static Dictionary<Factions, List<Targetable>> TargetsByFaction = new Dictionary<Factions, List<Targetable>>();

    private static Factions[] allFactions;

    public static void ResetState()
    {
        TargetsByFaction = new Dictionary<Factions, List<Targetable>>();
        allFactions = null;
    }

    public void Init()
    {
        ResetState();

        allFactions = (Factions[])Enum.GetValues(typeof(Factions));

        foreach (Factions f in allFactions)
        {
            TargetsByFaction[f] = new List<Targetable>();
        }
    }

    public static List<Targetable> GetNearestTargets(Vector2 pos, Factions curFaction)
    {
        //oh right, we can make like an 'warring manager' or whatever, right, to track which factions are aggroed
        List<Targetable> candidates = new List<Targetable>();

        foreach (Factions f in allFactions)
        {
            //replace with aggro manager? Maybe? Actually maybe unnecessary. 
            if (f != curFaction)
            {
                candidates.AddRange(TargetsByFaction[f]);
            }
        }

        if (candidates.Count == 0)
        {
            return new List<Targetable>();
        }

        for (int i = candidates.Count - 1; i >= 0; i--)
        {
            if (candidates[i].IsAlive() == false)
            {
                candidates.RemoveAt(i);
            }
        }

        candidates.Sort(new TargetCompareClass(pos));

        return candidates;
    }

    private class TargetCompareClass : IComparer<Targetable>
    {
        private Vector2 pos;

        public TargetCompareClass(Vector2 p) 
        {
            pos = p; 
        }

        public int Compare(Targetable a, Targetable b)
        {
            float dif = Vector2.Distance(a.GetMyPosition(), pos) - Vector2.Distance(b.GetMyPosition(), pos);
            return (int)(Mathf.Sign(dif) * Mathf.CeilToInt(Mathf.Abs(dif)));
        }
    }

    public static Targetable GetNearestTarget(Vector2 position, Factions curFaction)
    {
        List<Targetable> targets = GetNearestTargets(position, curFaction);
        if (targets.Count == 0)
        {
            return null; 
        }
        return targets[0];
    }

    public static void RemoveTarget(Targetable t)
    {
        TargetsByFaction[t.GetMyFaction()].Remove(t);
    }

    public static void AddTargetable(Targetable t)
    {
        if (!TargetsByFaction.ContainsKey(t.GetMyFaction())) 
        {
            throw new System.Exception("The dictionary does not contain a key for the faction " + t.GetMyFaction());
        }
        TargetsByFaction[t.GetMyFaction()].Add(t);
    }
}
