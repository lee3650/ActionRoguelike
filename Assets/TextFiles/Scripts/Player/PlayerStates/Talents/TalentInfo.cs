using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentInfo : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] bool reusable;
    [SerializeField] bool isActiveTalent;
    [SerializeField] bool upgradable;
    [SerializeField] bool isUpgrade;
    [SerializeField] int upgradeTier;
    [SerializeField] TalentPolicy TalentPolicy;
    [Tooltip("Only used if isActive is set to true")]
    [SerializeField] State activeState; 

    public void ApplyUpgrade()
    {
        TalentPolicy.ApplyUpgrade(upgradeTier); 
    }

    public void ApplyPolicy()
    {
        TalentPolicy.ApplyPolicy();
    }

    public State ActiveState
    {
        get
        {
            return activeState; 
        }
    }

    public bool Upgradable
    {
        get
        {
            return upgradable;
        }
    }

    public bool IsUpgrade
    {
        get
        {
            return isUpgrade; 
        }
    }

    public bool IsActiveTalent
    {
        get
        {
            return isActiveTalent;
        }
    }

    public bool Reusable
    {
        get
        {
            return reusable;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }
}
