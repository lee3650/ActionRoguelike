using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class SpreadManager : MonoBehaviour, Initializable
{
    private Dictionary<Factions, List<SpreadTarget>> Targets = new Dictionary<Factions, List<SpreadTarget>>();

    private static SpreadManager instance;

    const float spreadRange = 5f; 

    public void Init()
    {
        Targets = new Dictionary<Factions, List<SpreadTarget>>();

        foreach (Factions f in System.Enum.GetValues(typeof(Factions)))
        {
            Targets[f] = new List<SpreadTarget>();
        }

        instance = this; 
    }

    private void RegisterTarget(SpreadTarget target)
    {
        Targets[target.GetMyFaction()].Add(target); 
    }

    private List<SpreadTarget> GetNearbyTargets(Factions blacklist, Vector2 pos, SignalType signal)
    {
        List<SpreadTarget> result = new List<SpreadTarget>();
        List<SpreadTarget> search = new List<SpreadTarget>();

        Factions[] factions = (Factions[])System.Enum.GetValues(typeof(Factions));

        foreach (Factions f in factions)
        {
            if (f != blacklist)
            {
                search.AddRange(Targets[f]);
            }
        }

        search.OrderBy<SpreadTarget, float>(s => Vector2.Distance(s.GetPosition(), pos));
        //huh

        foreach (SpreadTarget s in search)
        {
            if (s.CanReceiveSpread(signal) && Vector2.Distance(s.GetPosition(), pos) < spreadRange)
            {
                result.Add(s);
            }
        }

        return result; 
    } 

    private void RemoveSpreadTarget(SpreadTarget t)
    {
        Targets[t.GetMyFaction()].Remove(t);
    }

    public static void RemoveTarget(SpreadTarget target)
    {
        instance.RemoveSpreadTarget(target);
    }

    public static List<SpreadTarget> GetTargets(Factions f, Vector2 pos, SignalType signal)
    {
        return instance.GetNearbyTargets(f, pos, signal); 
    }

    public static void RegisterSpreadTarget(SpreadTarget target)
    {
        instance.RegisterTarget(target);
    }
}
