using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedTalentManager : MonoBehaviour, Initializable
{
    [SerializeField] List<TalentPolicy> DefaultPolicies = new List<TalentPolicy>(); 
    
    public static List<TalentPolicy> UnlockedPolicies = new List<TalentPolicy>();

    public void UnlockTalent(TalentPolicy p)
    {
        UnlockedPolicies.Add(p); 
    }

    public void Init()
    {
        UnlockedPolicies.AddRange(DefaultPolicies); 
    }
}
