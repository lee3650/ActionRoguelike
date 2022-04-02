using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp : TalentPolicy, Dependency<HealthManager>
{
    [SerializeField] float healthIncrease; 
    HealthManager hm;

    public void InjectDependency(HealthManager h)
    {
        hm = h;
    }

    public override void ApplyPolicy()
    {
        hm.IncreaseMaxHealth(healthIncrease);
    }

    public override void UndoPolicy()
    {
        hm.DecreaseMaxHealth(healthIncrease);
        RemoveTalentAndUndoUpgrades();
    }
}
