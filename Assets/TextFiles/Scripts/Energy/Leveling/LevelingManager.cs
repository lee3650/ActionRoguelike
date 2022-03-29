using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingManager : MonoBehaviour, Initializable
{
    [SerializeField] List<TalentPolicy> UpgradeOptions;
    [SerializeField] PlayerGetter PlayerGetter;
    private TalentManager TalentManager;

    private List<TalentPolicy> furlough = new List<TalentPolicy>();

    private const int talentsToShow = 3;

    public void Init()
    {
        PlayerGetter.PlayerReady += PlayerReady;
    }

    private void PlayerReady(Transform obj)
    {
        TalentManager = obj.GetComponent<TalentManager>();
    }

    public List<TalentPolicy> GetUpgradeOptions()
    {
        //0, 1, or 2
        int numUpgrades = Random.Range(0, 3);

        List<TalentPolicy> upgrades = TalentManager.GetUpgradableTalents(numUpgrades);

        //upgrades might be < numUpgrades
        int newTalents = talentsToShow - upgrades.Count;

        List<TalentPolicy> result = new List<TalentPolicy>();
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

    public void UpgradeSelected(TalentPolicy t)
    {
        foreach (TalentPolicy f in furlough)
        {
            UpgradeOptions.Add(f);
        }

        furlough = new List<TalentPolicy>();

        TalentManager.ApplyTalent(t);

        if (!t.Reusable)
        {
            UpgradeOptions.Remove(t);
        }
    }
}
