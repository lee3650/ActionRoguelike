using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeOptionSupplier : MonoBehaviour
{
    public abstract List<TalentPolicy> GetUpgradeOptions();

    public abstract List<TalentPolicy> GetUpgradesForTalent(TalentPolicy tp);
}
