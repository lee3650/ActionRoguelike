using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI BodyText; 
    private System.Action<TalentPolicy> SelectedAction;
    private TalentPolicy MyTalent; 

    public void DisplayUpgrade(TalentPolicy talent, System.Action<TalentPolicy> selectedAction)
    {
        SelectedAction = selectedAction;
        MyTalent = talent;
        TitleText.text = talent.Title;
        BodyText.text = talent.Description;
    }

    public void OnClick()
    {
        SelectedAction(MyTalent);
    }
}
