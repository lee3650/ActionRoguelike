using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActiveTalent : TalentPolicy, Dependency<ActiveTalentManager>
{
    private ActiveTalentManager ActiveTalentManager;

    public void InjectDependency(ActiveTalentManager at)
    {
        ActiveTalentManager = at;
    }

    [SerializeField] State State;

    public override void ApplyPolicy()
    {
        ActiveTalentManager.AddTalent(State);
    }
}
