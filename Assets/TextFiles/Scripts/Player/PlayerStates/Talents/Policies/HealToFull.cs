using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealToFull : TalentPolicy, Initializable, Dependency<HealthManager>
{
    HealthManager hm;

    public void InjectDependency(HealthManager h)
    {
        hm = h;
    }

    public void Init()
    {
        Upgradable = false;
    }

    public override void ApplyPolicy()
    {
        hm.Heal(10000f);
    }
}
