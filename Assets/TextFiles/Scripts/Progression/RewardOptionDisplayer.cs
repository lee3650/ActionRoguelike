using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RewardOptionDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] TextMeshProUGUI CostText;

    private RewardOption CurrentOption;

    public void DisplayReward(RewardOption option)
    {
        TitleText.text = option.GetTitle();
        DescriptionText.text = option.GetDescription();
        CostText.text = string.Format("Buy for {0} woolongs", option.Cost);
        CurrentOption = option;
        gameObject.SetActive(true); 
    }

    public void BuyReward()
    {
        if (CurrentOption.CanUnlock() && MetaCurrencyManager.Balance >= CurrentOption.Cost)
        {
            CurrentOption.Unlocked = true;
            CurrentOption.UnlockReward();
            MetaCurrencyManager.Balance -= CurrentOption.Cost;
        }
    }
}
