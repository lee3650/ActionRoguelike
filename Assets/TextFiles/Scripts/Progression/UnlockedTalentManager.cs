using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedTalentManager : MonoBehaviour
{
    [SerializeField] List<TalentPolicy> DefaultPolicies = new List<TalentPolicy>();
    [SerializeField] ProgressionOptionSupplier ProgressionOptionSupplier;
    [SerializeField] TalentIDManager TalentIDManager;
    
    public static List<TalentPolicy> UnlockedPolicies = new List<TalentPolicy>();

    /// <summary>
    /// This is called before the player enters the game in order to correctly assign unlocked policies to what should appear in the pool of available talents
    /// </summary>
    public void AddAvailableTalents()
    {
        UnlockedPolicies = new List<TalentPolicy>();
        UnlockedPolicies.AddRange(DefaultPolicies);
        foreach (TalentPolicy tp in ProgressionOptionSupplier.GetUpgradeOptions())
        {
            UnlockedPolicies.Add(TalentIDManager.GetPrefab(tp.ID));
        }
    }
}