using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SingleTalentDisplay : MonoBehaviour
{
    [SerializeField] Button RemoveButton;
    [SerializeField] TextMeshProUGUI UpgradeName;
    [SerializeField] TextMeshProUGUI UpgradeDescription;
    [SerializeField] TextMeshProUGUI UpgradeCost;

    private System.Action<TalentPolicy> RemoveClicked;

    private TalentPolicy myTalent;

    public void DisplayTalent(TalentPolicy tp)
    {
        myTalent = tp;
        UpgradeName.text = tp.Title;
        UpgradeDescription.text = tp.Description;
        if (tp.GetCost() > 0)
        {
            UpgradeCost.text = "Cost: " + tp.GetCost() + " scrap";
        } else
        {
            UpgradeCost.text = "";
        }
    }

    public void ShowRemoveButton(System.Action<TalentPolicy> tp)
    {
        RemoveClicked = tp;
        RemoveButton.gameObject.SetActive(true);
    }

    public void RemoveButtonClicked()
    {
        RemoveClicked?.Invoke(myTalent);
    }
}
