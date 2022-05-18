using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionOptionSupplier : UpgradeOptionSupplier, Initializable
{
    [SerializeField] private List<TalentPolicy> AvailablePrefabs = new List<TalentPolicy>();
    private List<TalentPolicy> AvailableTalents = new List<TalentPolicy>();
    [SerializeField] private List<TalentAndUpgrades> StartingUpgrades = new List<TalentAndUpgrades>();
    [SerializeField] private int AvailableScrap = 15;

    public static Dictionary<int, List<int>> AppliedUpgrades = new Dictionary<int, List<int>>();

    private Dictionary<int, List<int>> Upgrades = new Dictionary<int, List<int>>();

    public static List<int> StartingTalents = new List<int>();
    public static List<Vector2Int> StartingPositions = new List<Vector2Int>();

    public void Init()
    {
        AppliedUpgrades = new Dictionary<int, List<int>>();
        Upgrades = new Dictionary<int, List<int>>();
        StartingTalents = new List<int>();
        StartingPositions = new List<Vector2Int>();

        foreach (TalentPolicy t in AvailablePrefabs)
        {
            AvailableTalents.Add(Instantiate(t));
        }

        foreach (TalentAndUpgrades tu in StartingUpgrades)
        {
            foreach (int id in tu.Upgrades)
            {
                UnlockUpgrade(tu.Parent, id);
            }
        }
    }
    
    public void AppliedUpgrade(TalentPolicy upgrade)
    {
        upgrade.Parent.AppliedUpgrade(upgrade);
        Upgrades[upgrade.Parent.ID].Remove(upgrade.ID);
        
        if (AppliedUpgrades.ContainsKey(upgrade.Parent.ID))
        {
            AppliedUpgrades[upgrade.Parent.ID].Add(upgrade.ID);
        } else
        {
            AppliedUpgrades[upgrade.Parent.ID] = new List<int>() { upgrade.ID };
        }

        AvailableScrap -= upgrade.GetCost();
        print("applying upgrade: " + upgrade.ID);
    }

    public void RemovePolicy(TalentPolicy policy)
    {
        if (!policy.IsUpgrade)
        {
            int i = StartingTalents.IndexOf(policy.ID);
            StartingPositions.RemoveAt(i);
            StartingTalents.RemoveAt(i);

            AvailableScrap += policy.GetCost();

            AvailableTalents.Add(policy);
        }
        else
        {
            TalentPolicy parent = policy.Parent;
            parent.UnappliedUpgrade(policy);

            AvailableScrap += policy.GetCost();

            Upgrades[parent.ID].Add(policy.ID);
            AppliedUpgrades[parent.ID].Remove(policy.ID);

        }
    }

    public void AppliedPolicy(TalentPolicy policy, Vector2Int index)
    {
        StartingTalents.Add(policy.ID);
        StartingPositions.Add(index);

        AvailableScrap -= policy.GetCost();

        AvailableTalents.Remove(policy);

        printAvailableTalents();
        printStartingTalents();
    }

    public void AddAvailableTalent(TalentPolicy tp)
    {
        AvailableTalents.Add(Instantiate(tp));
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

        foreach (int tp in StartingTalents)
        {
            print(tp);
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
        if (Upgrades.TryGetValue(tp.ID, out List<int> val))
        {
            print("returning equippable upgrades for talent " + tp.Title + " vals " + val.Count);
            return tp.GetEquippableUpgrades(val);
        }
        print("could not find upgrades for talent " + tp.Title);
        return new List<TalentPolicy>();
    }

    private TalentPolicy FindPolicyById(int id)
    {
        foreach (TalentPolicy tp in AvailableTalents)
        {
            if (tp.ID == id)
            {
                return tp;
            }
        }
        return null;
    }

    public void UnlockUpgrade(TalentPolicy parent, int upgrade)
    {
        parent = FindPolicyById(parent.ID);
        parent.AddUpgrade(upgrade);
        
        if (Upgrades.ContainsKey(parent.ID))
        {
            Upgrades[parent.ID].Add(upgrade);
        } else
        {
            Upgrades[parent.ID] = new List<int>() { upgrade };
        }
    }
}