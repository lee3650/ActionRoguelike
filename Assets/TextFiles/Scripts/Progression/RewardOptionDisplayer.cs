using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RewardOptionDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] TextMeshProUGUI CostText;
    [SerializeField] GameObject BuyButton;

    private RewardOption CurrentOption;

    public void DisplayReward(RewardOption option)
    {
        TitleText.text = option.GetTitle();
        DescriptionText.text = option.GetDescription();
        CostText.text = string.Format("Buy for {0} woolongs", option.Cost);
        CurrentOption = option;
        BuyButton.SetActive(true);
        if (option.Unlocked)
        {
            BuyButton.SetActive(false);  
        } 
        gameObject.SetActive(true); 
    }

    public void BuyReward()
    {
        if (CurrentOption.CanUnlock() && MetaCurrencyManager.Balance >= CurrentOption.Cost)
        {
            CurrentOption.Unlocked = true;
            CurrentOption.UnlockReward();
            MetaCurrencyManager.SpendBalance(CurrentOption.Cost);
            BuyButton.SetActive(false); 
        } else
        {
            print(string.Format("Failed because can unlock is {0}, balance is {1} and cost is {2}", CurrentOption.CanUnlock(), MetaCurrencyManager.Balance, CurrentOption.Cost));
        }
    }
}
