using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionModuleGrid : ModuleGrid, Initializable
{
    [SerializeField] GridClickHandler GridClickHandler;
    [SerializeField] ProgressionOptionSupplier OptionSupplier;
    [SerializeField] int Width, Height;
    [SerializeField] PopupText PopupText;

    public void Init()
    {
        Grid.CreateGrid(Width, Height);
    }

    public override void ApplyUpgrade(TalentPolicy upgrade)
    {
        print("applying upgrade in module grid: " + upgrade.Title);
        OptionSupplier.AppliedUpgrade(upgrade);

        ShowUpgrades(upgrade.Parent);

        print("upgrade parent applied upgrades count: " + upgrade.Parent.GetAppliedUpgrades().Count);
        print("upgrade parent applied upgrade 0: " + upgrade.Parent.GetAppliedUpgrades()[0].Description);
    }

    public void RemovePolicy(TalentPolicy tp)
    {
        Grid.RemovePolicy(tp);
        if (!tp.IsUpgrade)
        {
            UpgradePoller.SelectPolicy(null);
            LastSelected = null;
        } 
        else
        {
            UpgradePoller.RefreshPolicy();
        }
    }

    public void OnFailedPurchase()
    {
        PopupText.Show("Can't afford!", 0f);
    }

    public bool CanAddTalent(TalentPolicy tp)
    {
        return OptionSupplier.CanAffordTalent(tp);
    }

    public override List<SelectionAction> GetSelectionActions(TalentPolicy lastSelected, TalentPolicy newClick)
    {
        return GridClickHandler.HandleClick(lastSelected, newClick, new List<TalentPolicy>() { newClick }, false);
    }

    public override TalentPolicy GetDefaultTalentPolicy()
    {
        return null; //After deselection, we shouldn't show anything
    }

    public override void WriteToGrid(TalentPolicy policy, Vector3 gridPos)
    {
        Grid.AddGridElement(policy, gridPos);
        OptionSupplier.AppliedPolicy(policy, UtilityFunctions.RoundVectorToInt(Grid.GetItemIndex(gridPos)));
    }
}
