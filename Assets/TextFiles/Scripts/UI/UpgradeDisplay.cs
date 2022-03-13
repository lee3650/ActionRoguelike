using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI BodyText; 
    private System.Action<TalentInfo> SelectedAction;
    private TalentInfo MyTalent; 

    public void DisplayUpgrade(TalentInfo talent, System.Action<TalentInfo> selectedAction)
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
