using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingManager : MonoBehaviour
{
    [SerializeField] List<TalentInfo> UpgradeOptions;
    [SerializeField] TalentManager TalentManager;

    private List<TalentInfo> furlough = new List<TalentInfo>();

    private const int talentsToShow = 3;

    public List<TalentInfo> GetUpgradeOptions()
    {
        int numUpgrades = Random.Range(0, 100) < 50f ? 1 : 2;

        List<TalentInfo> upgrades = TalentManager.GetUpgradableTalents(numUpgrades);

        //upgrades might be < numUpgrades
        int newTalents = talentsToShow - upgrades.Count;

        List<TalentInfo> result = new List<TalentInfo>();
        result.AddRange(upgrades);

        for (int i = 0; i < newTalents; i++)
        {
            if (UpgradeOptions.Count == 0)
            {
                break;
            }

            int op = Random.Range(0, UpgradeOptions.Count);
            furlough.Add(UpgradeOptions[op]);
            result.Add(UpgradeOptions[op]);
            UpgradeOptions.RemoveAt(op);
        }

        return result; 
    }

    public void UpgradeSelected(TalentInfo t)
    {
        foreach (TalentInfo f in furlough)
        {
            if (f != t)
            {
                UpgradeOptions.Add(f);
            }
        }

        furlough = new List<TalentInfo>();

        TalentManager.ApplyTalent(t);

        if (!t.Reusable)
        {
            UpgradeOptions.Remove(t);
        }
    }
}
