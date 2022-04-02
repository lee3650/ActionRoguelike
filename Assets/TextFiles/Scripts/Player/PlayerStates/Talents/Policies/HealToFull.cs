using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealToFull : TalentPolicy, Initializable, Dependency<HealthManager>
{
    HealthManager hm;

    private float healAmt; 

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
        float max = hm.GetCurHealth() / (hm.GetHealthPercentage());
        healAmt = max - hm.GetCurHealth();
        hm.Heal(healAmt + 1);
    }

    public override void UndoPolicy()
    {
        hm.TakeDamage(healAmt);
        RemoveTalentAndUndoUpgrades();
    }
}
