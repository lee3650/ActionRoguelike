using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour, Initializable
{
    private static Dictionary<Factions, List<Targetable>> TargetsByFaction = new Dictionary<Factions, List<Targetable>>();

    private static Factions[] allFactions;

    public void Init()
    {
        allFactions = (Factions[])Enum.GetValues(typeof(Factions));

        foreach (Factions f in allFactions)
        {
            TargetsByFaction[f] = new List<Targetable>();
        }
    }

    public static Targetable GetNearestTarget(Vector2 position, Factions curFaction)
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
            return null; 
        }

        Targetable nearest = candidates[0];
        float minDistance = Vector2.Distance(position, nearest.GetMyPosition());
        for (int i = 0; i < candidates.Count; i++)
        {
            float newDist = Vector2.Distance(position, candidates[i].GetMyPosition());
            if (newDist < minDistance)
            {
                nearest = candidates[i];
                minDistance = newDist; 
            }
        }

        return nearest; 
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
