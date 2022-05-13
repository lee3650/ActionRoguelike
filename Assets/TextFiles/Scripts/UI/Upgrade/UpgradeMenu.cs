using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] UpgradeOptionSupplier OptionSupplier;
    [SerializeField] GridController Menu;

    private List<TalentPolicy> PolicyOptions;

    public void ShowPreviousOptions()
    {
        Menu.DisplayModules(PolicyOptions);
    }

    public void ShowUpgrades(TalentPolicy policy)
    {
        Menu.DisplayUpgrades(OptionSupplier.GetUpgradesForTalent(policy));
    }

    public void ShowNewOptions()
    {
        PolicyOptions = OptionSupplier.GetUpgradeOptions();
        Menu.DisplayModules(PolicyOptions);
    }
}
