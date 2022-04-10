using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardButton : MonoBehaviour, Initializable
{
    public RewardOption RewardOption;
    public RewardButton Parent; 
    [SerializeField] RewardOptionDisplayer RewardOptionDisplayer;
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI CostText;
        
    public void Init()
    {
        TitleText.text = RewardOption.GetTitle();
        CostText.text = string.Format("{0} woolongs", RewardOption.Cost); 
        if (Parent != null)
        {
            RewardOption.Prerequisite = Parent.RewardOption;
        }
    }

    public void OnClick()
    {
        RewardOptionDisplayer.DisplayReward(RewardOption); 
    }
}
