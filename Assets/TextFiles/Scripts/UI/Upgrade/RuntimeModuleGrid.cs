using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuntimeModuleGrid : ModuleGrid, Initializable, IPointerClickHandler
{
    [SerializeField] int Width;
    [SerializeField] int Height;
    [SerializeField] XPManager XPManager;
    [SerializeField] GridClickHandler GridClickHandler;

    private List<TalentPolicy> UpgradeMask = new List<TalentPolicy>();

    public void Init()
    {
        Grid.CreateGrid(Width, Height);

        XPManager.ModuleComplete += ModuleComplete;
    }

    private void ModuleComplete()
    {
        UpgradePoller.SetDefaultPolicy(null);

        Grid.ResetElementStates();

        UpgradeMask = Grid.GetRandomUpgradeMask();

        Grid.ApplyEventToElements(UpgradeMask, GridElementEvent.Upgradable);
    }

    private void SetNewPolicy(TalentPolicy policy)
    {
        UpgradePoller.ResetPolls();
        UpgradePoller.SetDefaultPolicy(policy);
        Grid.ResetElementStates();
    }

    public override void ApplyUpgrade(TalentPolicy upgrade)
    {
        XPManager.SetCurrentPolicy(upgrade);
        SetNewPolicy(upgrade);
    }

    public override void WriteToGrid(TalentPolicy policy, Vector3 worldPos)
    {
        Grid.AddGridElement(policy, worldPos);

        Prereq.Assert(policy.Progress == 0, "Policy progress was not zero for policy " + policy.Title);
        Prereq.Assert(policy.GetCost() > 0, "Policy cost was <= zero for policy " + policy.Title);
        XPManager.SetCurrentPolicy(policy);

        SetNewPolicy(policy);
    }

    public override List<SelectionAction> GetSelectionActions(TalentPolicy lastSelected, TalentPolicy newClick)
    {
        return GridClickHandler.HandleClick(lastSelected, newClick, UpgradeMask, XPManager.HasPolicyInProgress());
    }

    public override TalentPolicy GetDefaultTalentPolicy()
    {
        return XPManager.GetCurrentPolicy();
    }
}
