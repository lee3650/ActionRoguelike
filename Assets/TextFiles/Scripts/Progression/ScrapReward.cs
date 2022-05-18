using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapReward : RewardOption
{
    [SerializeField] ProgressionOptionSupplier OptionSupplier;
    [SerializeField] int Amount = 3; 
    public override void UnlockReward()
    {
        OptionSupplier.IncreaseAvailableScrap(Amount);
    }

    public override string GetTitle()
    {
        return "Increase Available Scrap";
    }

    public override string GetDescription()
    {
        return "Increase available scrap for your loadout by " + Amount;
    }
}
