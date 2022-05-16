using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionOptionSupplier : UpgradeOptionSupplier, Initializable
{
    [SerializeField] private List<TalentPolicy> AvailableTalents = new List<TalentPolicy>();
    [SerializeField] private List<TalentAndUpgrades> StartingUpgrades = new List<TalentAndUpgrades>();
    [SerializeField] private int AvailableScrap = 15;

    private Dictionary<TalentPolicy, List<TalentPolicy>> Upgrades = new Dictionary<TalentPolicy, List<TalentPolicy>>();

    public static List<TalentPolicy> StartingTalents = new List<TalentPolicy>();
    public static List<Vector2Int> StartingPositions = new List<Vector2Int>();

    public void Init()
    {
        foreach (TalentAndUpgrades tu in StartingUpgrades)
        {
            foreach (TalentPolicy tp in tu.Upgrades)
            {
                UnlockUpgrade(tu.Parent, tp);
            }
        }
    }
    
    public void AppliedUpgrade(TalentPolicy upgrade)
    {
        upgrade.Parent.AppliedUpgrade(upgrade);
        Upgrades[upgrade.Parent].Remove(upgrade);
        AvailableScrap -= upgrade.GetCost();
    }

    public void RemovePolicy(TalentPolicy policy)
    {
        if (!policy.IsUpgrade)
        {
            int i = StartingTalents.IndexOf(policy);
            StartingPositions.RemoveAt(i);
            StartingTalents.RemoveAt(i);

            AvailableScrap += policy.GetCost();

            AvailableTalents.Add(policy);
        }
    }

    public void AppliedPolicy(TalentPolicy policy, Vector2Int index)
    {
        StartingTalents.Add(policy);
        StartingPositions.Add(index);

        AvailableScrap -= policy.GetCost();

        AvailableTalents.Remove(policy);

        printAvailableTalents();
        printStartingTalents();
    }

    public void AddAvailableTalent(TalentPolicy tp)
    {
        AvailableTalents.Add(tp);
    }

    public int GetAvailableScrap()
    {
        return AvailableScrap;
    }

    private void printAvailableTalents()
    {
        print("available talents");
        foreach (TalentPolicy tp in AvailableTalents)
        {
            print(tp.Title);
        }
    }

    private void printStartingTalents()
    {
        print("starting talents");

        foreach (TalentPolicy tp in StartingTalents)
        {
            print(tp.Title);
        }
    }

    public bool CanAffordTalent(TalentPolicy policy)
    {
        return AvailableScrap >= policy.GetCost();
    }

    public override List<TalentPolicy> GetUpgradeOptions()
    {
        return AvailableTalents;
    }

    public override List<TalentPolicy> GetUpgradesForTalent(TalentPolicy tp)
    {
        if (Upgrades.TryGetValue(tp, out List<TalentPolicy> val))
        {
            return val;
        }
        return new List<TalentPolicy>();
    }

    public void UnlockUpgrade(TalentPolicy parent, TalentPolicy upgrade)
    {
        parent.AddUpgrade(upgrade);
        upgrade.Parent = parent; 
        if (Upgrades.ContainsKey(parent))
        {
            Upgrades[parent].Add(upgrade);
        } else
        {
            Upgrades[parent] = new List<TalentPolicy>() { upgrade };
        }
    }
}