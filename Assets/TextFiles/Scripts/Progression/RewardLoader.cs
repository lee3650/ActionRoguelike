using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLoader : MonoBehaviour, Initializable
{
    [SerializeField] RewardOption[] AllRewards;

    public static List<int> UnlockedRewards = new List<int>();

    public void Init()
    {
        foreach (int i in UnlockedRewards)
        {
            AllRewards[i].Unlocked = true;
            AllRewards[i].UnlockReward();
        }
    }

    public void UnlockedReward(RewardOption ro)
    {
        for (int i = 0; i < AllRewards.Length; i++)
        {
            if (AllRewards[i] == ro)
            {
                UnlockedRewards.Add(i);
                return; 
            }
        }
    }
}
