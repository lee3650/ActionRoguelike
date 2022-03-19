using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentPolicy : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] bool reusable;
    [SerializeField] private bool upgradable = false;
    [SerializeField] bool isUpgrade = false;
    [SerializeField] private bool isActiveTalent;
    [SerializeField] List<TalentPolicy> Upgrades;
    [SerializeField] bool RandomizeUpgrades; 

    public bool IsActiveTalent
    {
        get
        {
            return isActiveTalent;
        }
    }

    public bool IsUpgrade
    {
        get { return isUpgrade; }
    }

    public TalentPolicy Parent
    {
        get;
        set;
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

    public virtual string Description
    {
        get
        {
            return description;
        }
    }

    public abstract void ApplyPolicy();

    public TalentPolicy GetNextUpgrade()
    {
        if (RandomizeUpgrades)
        {
            Upgrades = (List<TalentPolicy>)UtilityRandom.SortByRandom(Upgrades);
        }
        TalentPolicy result = Upgrades[0];
        result.Parent = this;
        return result;
    }

    public void AppliedNextUpgrade()
    {
        Upgrades.RemoveAt(0);
        if (Upgrades.Count == 0)
        {
            Upgradable = false;
        }
    }

    public bool Upgradable
    {
        get
        {
            return upgradable;
        }
        protected set
        {
            upgradable = value; 
        }
    }
}
