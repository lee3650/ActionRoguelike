using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionTalentDisplayer : TalentDisplayer
{
    [SerializeField] SingleTalentDisplay SingleTalentDisplay;
    [SerializeField] Transform TalentGroup;
    [SerializeField] ProgressionController ProgressionController;

    private List<SingleTalentDisplay> oldView = new List<SingleTalentDisplay>();

    public override void DisplayTalent(TalentPolicy tp)
    {
        DestroyOldView();
        List<TalentPolicy> talents = UtilityFunctions.CopyList<TalentPolicy>(tp.GetAppliedUpgrades());
        talents.Insert(0, tp);
        oldView = DisplayListOfTalents(talents, tp, false);
    }

    public override void DisplaySelectedTalent(TalentPolicy tp)
    {
        DestroyOldView();
        List<TalentPolicy> talents = UtilityFunctions.CopyList<TalentPolicy>(tp.GetAppliedUpgrades());
        talents.Insert(0, tp);
        oldView = DisplayListOfTalents(talents, tp, true);
    }

    private void DestroyOldView()
    {
        foreach (SingleTalentDisplay std in oldView)
        {
            Destroy(std.gameObject);
        }
        oldView = new List<SingleTalentDisplay>();
    }

    private List<SingleTalentDisplay> DisplayListOfTalents(List<TalentPolicy> talents, TalentPolicy t, bool tryShowRemove)
    {
        List<SingleTalentDisplay> result = new List<SingleTalentDisplay>();

        foreach (TalentPolicy tp in talents)
        {
            SingleTalentDisplay std = Instantiate(SingleTalentDisplay, TalentGroup);
            std.DisplayTalent(t);

            if (tryShowRemove && t.GetCost() > 0)
            {
                std.ShowRemoveButton(ProgressionController.RemovePolicy);
            }
            
            result.Add(std);
        }

        return result; 
    }
}
