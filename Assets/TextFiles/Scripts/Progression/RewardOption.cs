using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RewardOption : MonoBehaviour
{
    public abstract string GetTitle(); 
    public abstract string GetDescription();
    public int Cost;
    public bool Unlocked;

    [Tooltip("This is assigned by the associated button's parent")]
    public RewardOption Prerequisite; 
    public bool CanUnlock()
    {
        return Prerequisite == null || Prerequisite.Unlocked; 
    }

    public abstract void UnlockReward();
}
