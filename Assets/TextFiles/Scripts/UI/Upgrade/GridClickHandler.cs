using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClickHandler : MonoBehaviour
{
    public List<SelectionAction> HandleClick(TalentPolicy LastSelected, TalentPolicy newClick, List<TalentPolicy> upgradeMask, bool policyInProgress)
    {
        List<SelectionAction> result = new List<SelectionAction>();

        if (LastSelected != null && (LastSelected == newClick || LastSelected != null && newClick == null))
        {
            //we need to deselect the last selected if it isn't null, and either you clicked the same thing twice or clicked on empty space
            result.Add(new SelectionAction(LastSelected, SelectionActionType.Deselect));

            if (!policyInProgress)
            {
                result.Add(new SelectionAction(null, SelectionActionType.ShowPreviousOptions));
            }
        }

        if (newClick != null && newClick != LastSelected)
        {
            //deselect last selected if necessary
            if (LastSelected != null)
            {
                result.Add(new SelectionAction(LastSelected, SelectionActionType.Deselect));
            }

            result.Add(new SelectionAction(null, SelectionActionType.ClearOptions));

            //select the newly clicked thing
            result.Add(new SelectionAction(newClick, SelectionActionType.Select));

            if (!policyInProgress && upgradeMask.Contains(newClick))
            {
                //show upgrades if there isn't a policy in progress and this is a member of the mask
                result.Add(new SelectionAction(newClick, SelectionActionType.ShowUpgrades));
            }
        }

        return result; 
    }
}

public struct SelectionAction
{
    public SelectionAction(TalentPolicy tp, SelectionActionType type)
    {
        Policy = tp;
        Type = type;
    }

    public TalentPolicy Policy;
    public SelectionActionType Type;
}

public enum SelectionActionType
{
    Select,
    Deselect,
    ShowUpgrades,
    ShowPreviousOptions,
    ClearOptions,
}
