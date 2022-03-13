using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    [SerializeField] private List<TalentPolicy> CurrentTalents;
    [SerializeField] ActiveTalentManager ActiveTalentManager;
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] Transform Player; 

    public List<TalentInfo> GetUpgradableTalents(int numUpgrades)
    {
        List<TalentInfo> result = new List<TalentInfo>();

        List<TalentPolicy> randomSort = (List<TalentPolicy>)UtilityRandom.SortByRandom(CurrentTalents);

        foreach (TalentPolicy i in randomSort)
        {
            if (i.Upgradable)
            {
                result.Add(i.GetNextUpgradeInfo());
                numUpgrades--;
                if (numUpgrades == 0)
                {
                    break;
                }
            }
        }

        return result; 
    }

    public void ApplyTalent(TalentInfo t)
    {
        if (t.IsUpgrade)
        {
            t.ApplyUpgrade();
        } else
        {
            print("injecting dependencies!");
            InjectionSet.InjectDependencies(t.transform);
            t.transform.parent = Player;
            t.transform.localPosition = Vector3.zero;
            if (t.IsActiveTalent)
            {
                ActiveTalentManager.AddTalent(t.ActiveState);
            } 
            else
            {
                t.ApplyPolicy(); 
            }
        }
    }
}
