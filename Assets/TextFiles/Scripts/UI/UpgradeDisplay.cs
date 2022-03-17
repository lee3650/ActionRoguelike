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
    private KeyFromTalent KeyFromTalent;

    public void DisplayUpgrade(TalentPolicy talent, System.Action<TalentPolicy> selectedAction, KeyFromTalent kft)
    {
        SelectedAction = selectedAction;
        MyTalent = talent;
        TitleText.text = talent.Title;
        string append = "";
        if (talent.IsActiveTalent)
        {
            append = string.Format("\n\nPress {0} to activate", kft.GetNextAvailableKey());
        }
        BodyText.text = talent.Description + append; 
    }

    public void OnClick()
    {
        SelectedAction(MyTalent);
    }
}
