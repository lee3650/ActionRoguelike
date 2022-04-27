using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealAbility : TalentPolicy, Dependency<ActiveTalentManager>
{
    [SerializeField] State HealState;
    private ActiveTalentManager ActiveTalentManager;
    public void InjectDependency(ActiveTalentManager atm)
    {
        ActiveTalentManager = atm; 
    }

    public override void ApplyPolicy()
    {
        ActiveTalentManager.AddTalent(HealState);
    }

    public override void UndoPolicy()
    {
        ActiveTalentManager.RemoveTalent(HealState);
        RemoveTalentAndUndoUpgrades();
    }
}
