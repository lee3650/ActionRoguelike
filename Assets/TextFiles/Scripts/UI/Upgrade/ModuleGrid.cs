using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ModuleGrid : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected UpgradePoller UpgradePoller;
    [SerializeField] UpgradeMenu UpgradeMenu;
    [SerializeField] RectTransform canvas;
    [SerializeField] protected GridDS Grid;

    protected TalentPolicy LastSelected = null;

    public (Vector3 gridPos, float dist) GetNearestGridItem(Vector3 worldPos)
    {
        return Grid.GetNearestGridItem(worldPos);
    }

    public bool IsPositionValid(Vector3 gridPos, TalentPolicy policy)
    {
        return Grid.IsPositionValid(gridPos, policy);
    }

    public bool CanUpgradeBeApplied(Vector3 gridPos, TalentPolicy parent)
    {
        return Grid.CanUpgradeBeApplied(gridPos, parent);
    }

    public abstract void WriteToGrid(TalentPolicy policy, Vector3 gridPos);

    public abstract void ApplyUpgrade(TalentPolicy upgrade);

    public abstract List<SelectionAction> GetSelectionActions(TalentPolicy lastSelected, TalentPolicy newClick);

    /// <summary>
    /// What should be shown when a talent is deselected? 
    /// </summary>
    public abstract TalentPolicy GetDefaultTalentPolicy();

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 globalPos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, eventData.pressEventCamera, out globalPos))
        {
            TalentPolicy newClick = Grid.GetPolicy(globalPos);

            List<SelectionAction> actions = GetSelectionActions(LastSelected, newClick);

            LastSelected = newClick;

            HandleSelectionActions(actions);
        }
    }

    private void HandleSelectionActions(List<SelectionAction> actions)
    {
        foreach (SelectionAction a in actions)
        {
            switch (a.Type)
            {
                case SelectionActionType.Deselect:
                    LastSelected = null;
                    DeselectTalent(a.Policy);
                    break;
                case SelectionActionType.Select:
                    SelectTalent(a.Policy);
                    break;
                case SelectionActionType.ShowUpgrades:
                    ShowUpgrades(a.Policy);
                    break;
                case SelectionActionType.ShowPreviousOptions:
                    ShowPreviousOptions();
                    break;
                case SelectionActionType.ClearOptions:
                    UpgradePoller.ResetPolls();
                    break;
            }
        }
    }

    private void SelectTalent(TalentPolicy tp)
    {
        UpgradePoller.SelectPolicy(tp);
        Grid.SendElementEvent(tp, GridElementEvent.Selected);
        LastSelected = tp;
    }

    private void ShowUpgrades(TalentPolicy tp)
    {
        UpgradePoller.ResetPolls();
        UpgradeMenu.ShowUpgrades(tp);
    }

    private void ShowPreviousOptions()
    {
        UpgradePoller.SelectPolicy(null);
        UpgradePoller.ResetPolls();
        UpgradeMenu.ShowPreviousOptions();
    }

    private void DeselectTalent(TalentPolicy tp)
    {
        Prereq.Assert(tp != null, "Deselected a null talent!");

        Grid.SendElementEvent(tp, GridElementEvent.Deselected);

        UpgradePoller.SelectPolicy(GetDefaultTalentPolicy());
    }
}