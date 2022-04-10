using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentReward : RewardOption
{
    [SerializeField] TalentPolicy MyPolicy; 

    public override void UnlockReward()
    {
        throw new System.NotImplementedException();
    }

    public override string GetTitle()
    {
        return MyPolicy.Title;
    }

    public override string GetDescription()
    {
        return string.Format("Add the policy {0} to the pool of available talents\n\n{1}", MyPolicy.Title, MyPolicy.Description); 
    }
}
