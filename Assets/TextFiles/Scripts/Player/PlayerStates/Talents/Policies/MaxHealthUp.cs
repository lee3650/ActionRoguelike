using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp : TalentPolicy, Dependency<HealthManager>
{
    [SerializeField] float healthIncrease; 
    HealthManager hm;

    public void InjectDependency(HealthManager h)
    {
        print("hm dependency injected!");
        hm = h;
    }

    public override void ApplyPolicy()
    {
        hm.IncreaseMaxHealth(healthIncrease);
    }
    public override void ApplyUpgrade(int index)
    {
        throw new System.Exception("Should not have applied an upgrade!");
    }

    public override TalentInfo GetNextUpgradeInfo()
    {
        return null; //?
    }
}
