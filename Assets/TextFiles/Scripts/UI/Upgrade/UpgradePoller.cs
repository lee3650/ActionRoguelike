using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradePoller : MonoBehaviour
{
    [SerializeField] List<MousePoll> MousePolls;
    [SerializeField] TextMeshProUGUI UpgradeName;
    [SerializeField] TextMeshProUGUI UpgradeDescription;
    [SerializeField] TextMeshProUGUI UpgradeCost;

    private void FixedUpdate()
    {
        bool found = false; 

        foreach (MousePoll mp in MousePolls)
        {
            if (mp.MousedOver)
            {
                TalentGetter tg = mp.GetComponent<TalentGetter>();
                found = true;
                UpgradeName.text = tg.Policy.Title;
                UpgradeDescription.text = tg.Policy.Description;
                UpgradeCost.text = "Cost: 7 scrap";
                break;
            }
        }

        if (!found)
        {
            UpgradeName.text = "";
            UpgradeDescription.text = "";
            UpgradeCost.text = "";
        }
    }
}
