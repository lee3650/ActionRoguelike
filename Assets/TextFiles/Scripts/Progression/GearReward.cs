using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearReward : RewardOption
{
    [SerializeField] Item Item;
    [SerializeField] UnlockedItemManager Manager; 

    public override string GetDescription()
    {
        return Item.GetDescription();
    }

    public override string GetTitle()
    {
        return Item.GetItemTitle(); 
    }

    public override void UnlockReward()
    {
        Manager.UnlockItem(Item); 
    }
}
