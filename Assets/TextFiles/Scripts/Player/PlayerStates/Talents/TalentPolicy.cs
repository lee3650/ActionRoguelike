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

    private TalentPolicy[,] Shape = null;
    
    protected List<TalentPolicy> AppliedUpgrades = new List<TalentPolicy>();
    protected TalentManager TM;

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
        AppliedUpgrades.Add(Upgrades[0]);
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
