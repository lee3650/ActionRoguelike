using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActiveTalent : TalentPolicy, Dependency<ActiveTalentManager>, Dependency<StatsList>
{
    private ActiveTalentManager ActiveTalentManager;
    [SerializeField] bool setStat = false;
    [SerializeField] string stat;
    [SerializeField] float amt;

    private StatsList PlayerStats; 

    public void InjectDependency(StatsList stats)
    {
        PlayerStats = stats; 
    }

    public void InjectDependency(ActiveTalentManager at)
    {
        ActiveTalentManager = at;
    }

    [SerializeField] State State;

    public override void ApplyPolicy()
    {
        ActiveTalentManager.AddTalent(State);
        if (setStat)
        {
            PlayerStats.AddToStat(stat, amt);
        }
    }

    public override void UndoPolicy()
    {
        ActiveTalentManager.RemoveTalent(State);
        RemoveTalentAndUndoUpgrades();
    }
}
