using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingManager : UpgradeOptionSupplier, Initializable
{
    [SerializeField] List<TalentPolicy> FallbackOptions;
    [SerializeField] PlayerGetter PlayerGetter;

    private List<TalentPolicy> UpgradeOptions = new List<TalentPolicy>(); 

    private TalentManager TalentManager;

    private List<TalentPolicy> furlough = new List<TalentPolicy>();

    private const int talentsToShow = 3;

    public event System.Action LevelingManagerReady = delegate { };

    public void Init()
    {
        PlayerGetter.PlayerReady += PlayerReady;

        List<TalentPolicy> talentSource = UnlockedTalentManager.UnlockedPolicies.Count > 0 ? UnlockedTalentManager.UnlockedPolicies : FallbackOptions;

        for (int i = 0; i < talentSource.Count; i++)
        {
            UpgradeOptions.Add(Instantiate(talentSource[i], transform));
        }
    }

    private void PlayerReady(Transform obj)
    {
        TalentManager = obj.GetComponent<TalentManager>();
        LevelingManagerReady();
    }

    public override List<TalentPolicy> GetUpgradeOptions()
    {
        List<TalentPolicy> result = new List<TalentPolicy>();

        for (int i = 0; i < talentsToShow; i++)
        {
            if (UpgradeOptions.Count == 0)
            {
                break;
            }

            int op = Random.Range(0, UpgradeOptions.Count);

            TalentPolicy tp = UpgradeOptions[op];

            if (tp.Reusable)
            {
                Prereq.Assert(!tp.Upgradable && !tp.IsUpgrade, "Talent policy " + tp.Title + " was both upgradable and reusable or was an upgrade and reusable!");
                tp = Instantiate(tp);
            }

            furlough.Add(tp);
            result.Add(tp);

            UpgradeOptions.RemoveAt(op);
        }

        return result; 
    }

    public override List<TalentPolicy> GetUpgradesForTalent(TalentPolicy talent)
    {
        return talent.GetNextUpgrades(2);
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
