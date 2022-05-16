using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedTalentManager : MonoBehaviour
{
    [SerializeField] List<TalentPolicy> DefaultPolicies = new List<TalentPolicy>();
    [SerializeField] ProgressionOptionSupplier ProgressionOptionSupplier;
    
    public static List<TalentPolicy> UnlockedPolicies = new List<TalentPolicy>();

    public void AddAvailableTalents()
    {
        UnlockedPolicies = new List<TalentPolicy>();
        UnlockedPolicies.AddRange(DefaultPolicies);
        UnlockedPolicies.AddRange(ProgressionOptionSupplier.GetUpgradeOptions());
    }
}
