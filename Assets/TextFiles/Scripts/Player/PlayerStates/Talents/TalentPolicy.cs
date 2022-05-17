using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentPolicy : MonoBehaviour, Dependency<TalentManager>
{
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] bool reusable;
    [SerializeField] private bool upgradable = false;
    [SerializeField] bool isUpgrade = false;
    [SerializeField] private bool isActiveTalent;
    [SerializeField] List<TalentPolicy> Upgrades;
    [SerializeField] bool RandomizeUpgrades;
    [SerializeField] bool[] TalentShape;
    [SerializeField] int TalentWidth;
    [SerializeField] int Cost;
    [SerializeField] private int id;

    /// <summary>
    /// This should never be modified (except through the inspector)
    /// </summary>
    [SerializeField] private List<TalentPolicy> AllEquippableUpgrades = new List<TalentPolicy>(); 

    private TalentPolicy[,] Shape = null;

    [SerializeField] protected List<TalentPolicy> AppliedUpgrades = new List<TalentPolicy>();
    protected TalentManager TM;

    void Awake()
    {
        Prereq.Assert(Cost != 0, "Cost was zero for talent policy " + title);
    }

    /// <summary>
    /// Don't modify this - use indexes to create a mask
    /// </summary>
    public List<TalentPolicy> GetAllEquippableUpgrades()
    {
        List<int> used = new List<int>();
        for (int i = 0; i < AllEquippableUpgrades.Count; i++)
        {
            Prereq.Assert(used.Contains(AllEquippableUpgrades[i].ID) == false, "ID was present already for upgrade " + AllEquippableUpgrades[i].Title);
            used.Add(AllEquippableUpgrades[i].ID);
        }

        return AllEquippableUpgrades;
    }

    public int ID
    {
        get
        {
            return id; 
        }
    }

    public TalentPolicy[,] GetShape()
    {
        if (isUpgrade)
        {
            return Parent.GetShape();
        }

        if (Shape != null)
        {
            return Shape; 
        }

        Shape = new TalentPolicy[TalentWidth, TalentShape.Length / TalentWidth];

        for (int i = 0; i < TalentShape.Length; i++)
        {
            Shape[i % TalentWidth, i / TalentWidth] = TalentShape[i] ? this : null;
        }

        return Shape; 
    }

    public int Progress
    {
        get;
        set;
    }

    public int GetCost()
    {
        return Cost; 
    }

    public void InjectDependency(TalentManager tm)
    {
        TM = tm; 
    }

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

    public abstract void UndoPolicy();

    protected void RemoveTalentAndUndoUpgrades()
    {
        TM.RemoveTalent(this);
        foreach (TalentPolicy tp in AppliedUpgrades)
        {
            tp.UndoPolicy();
        }
    }

    public void RandomizeUpgradeOrder()
    {
        if (RandomizeUpgrades)
        {
            Upgrades = (List<TalentPolicy>)UtilityRandom.SortByRandom(Upgrades);
        }
    }

    public List<TalentPolicy> GetNextUpgrades(int n)
    {
        List<TalentPolicy> result = new List<TalentPolicy>();

        for (int i = 0; i < n; i++)
        {
            if (i < Upgrades.Count)
            {
                Upgrades[i].Parent = this; 
                result.Add(Upgrades[i]);
            }
        }

        return result;
    }

    public List<TalentPolicy> GetAppliedUpgrades()
    {
        return AppliedUpgrades;
    }

    public void AppliedUpgrade(TalentPolicy t)
    {
        print("applying upgrade in policy: " + t.title);
        Prereq.Assert(Upgrades.Contains(t), "Tried to apply an upgrade not contained in upgrades: " + t.title + ", " + title);

        AppliedUpgrades.Add(t);
        t.Parent = this; 
        Upgrades.Remove(t);
        if (Upgrades.Count == 0)
        {
            Upgradable = false;
        }
    }

    public void AddUpgrade(int upgradeID)
    {
        TalentPolicy upgrade = GetEquippableUpgrade(upgradeID);
        Prereq.Assert(upgrade != null, "Upgrade with id " + upgradeID + " was null on talent " + title + " with equippable upgrades: " + AllEquippableUpgrades.Count);
        Upgrades.Add(upgrade);
        upgrade.Parent = this; 
    }

    public List<TalentPolicy> GetEquippableUpgrades(List<int> ids)
    {
        List<TalentPolicy> result = new List<TalentPolicy>();

        foreach (TalentPolicy tid in AllEquippableUpgrades)
        {
            if (ids.Contains(tid.ID))
            {
                result.Add(tid);
            }
        }

        return result; 
    }

    public TalentPolicy GetEquippableUpgrade(int id)
    {
        foreach (TalentPolicy ti in AllEquippableUpgrades)
        {
            if (ti.ID == id)
            {
                return ti;
            }
        }

        return null;
    }

    public void UnappliedUpgrade(TalentPolicy upgrade)
    {
        AppliedUpgrades.Remove(upgrade);
        Upgrades.Add(upgrade);
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
