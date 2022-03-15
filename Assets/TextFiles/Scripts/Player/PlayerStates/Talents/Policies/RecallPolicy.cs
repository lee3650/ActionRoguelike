using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallPolicy : TalentPolicy, Dependency<ActiveTalentManager>
{
    private ActiveTalentManager ActiveTalentManager;

    public void InjectDependency(ActiveTalentManager at)
    {
        ActiveTalentManager = at; 
    }

    [SerializeField] TalentPolicy[] Upgrades;
    [SerializeField] State State; 

    private int nextUpgrade = 0; 

    public override void ApplyPolicy()
    {
        ActiveTalentManager.AddTalent(State);
    }

    public override TalentPolicy GetNextUpgrade()
    {
        return Upgrades[nextUpgrade];
    }

    public override void AppliedNextUpgrade()
    {
        nextUpgrade++;
        if (nextUpgrade == Upgrades.Length)
        {
            Upgradable = false; 
        }
    }
}
