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
        print(string.Format("Option : {2}, prereq: {0}, unlocked: {1}", Prerequisite, Prerequisite == null ? "n/a" : "" + Prerequisite.Unlocked, name));
        return Prerequisite == null || Prerequisite.Unlocked; 
    }

    public abstract void UnlockReward();
}
