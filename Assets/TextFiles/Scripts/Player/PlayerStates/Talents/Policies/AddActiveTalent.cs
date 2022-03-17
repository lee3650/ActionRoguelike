using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActiveTalent : TalentPolicy, Dependency<ActiveTalentManager>
{
    [SerializeField] KeyFromTalent KeyFromTalent;
    [SerializeField] bool RandomizeUpgrades; 

    private ActiveTalentManager ActiveTalentManager;

    public void InjectDependency(ActiveTalentManager at)
    {
        ActiveTalentManager = at;
    }

    [SerializeField] List<TalentPolicy> Upgrades;
    [SerializeField] State State;

    public override void ApplyPolicy()
    {
        ActiveTalentManager.AddTalent(State);
    }

    public override string Description => base.Description + "\nPress " + KeyFromTalent.GetNextAvailableKey() + " to activate.";

    public override TalentPolicy GetNextUpgrade()
    {
        if (RandomizeUpgrades)
        {
            Upgrades = (List<TalentPolicy>)UtilityRandom.SortByRandom(Upgrades);
        }
        TalentPolicy result = Upgrades[0];
        result.Parent = this; 
        return result;
    }

    public override void AppliedNextUpgrade()
    {
        Upgrades.RemoveAt(0);
        if (Upgrades.Count == 0)
        {
            Upgradable = false;
        }
    }
}
