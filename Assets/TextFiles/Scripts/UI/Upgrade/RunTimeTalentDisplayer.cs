using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunTimeTalentDisplayer : TalentDisplayer
{
    [SerializeField] TextMeshProUGUI UpgradeName;
    [SerializeField] TextMeshProUGUI UpgradeDescription;
    [SerializeField] TextMeshProUGUI UpgradeCost;

    public override void DisplayTalent(TalentPolicy Policy)
    {
        UpgradeName.text = Policy.Title;
        UpgradeDescription.text = Policy.Description;
        UpgradeCost.text = GetCostText(Policy);

        foreach (TalentPolicy tp in Policy.GetAppliedUpgrades())
        {
            UpgradeDescription.text += string.Format("\n\nUpgrade: {0}\nCost: {1} scrap", tp.Description, tp.GetCost());
        }
    }

    public override void DisplaySelectedTalent(TalentPolicy tp)
    {
        //no difference for now
        DisplayTalent(tp);
    }
}
