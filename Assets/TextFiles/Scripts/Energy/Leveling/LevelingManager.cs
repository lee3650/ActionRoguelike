using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingManager : MonoBehaviour
{
    [SerializeField] List<TalentInfo> UpgradeOptions;
    [SerializeField] TalentManager TalentManager;

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
            result.Add(UpgradeOptions[Random.Range(0, UpgradeOptions.Count)]);
        }

        return result; 
    }

    public void UpgradeSelected(TalentInfo t)
    {
        TalentManager.ApplyTalent(t);

        if (!t.Reusable)
        {
            UpgradeOptions.Remove(t);
        }
    }
}
