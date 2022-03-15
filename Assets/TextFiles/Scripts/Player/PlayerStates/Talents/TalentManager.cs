using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    [SerializeField] private List<TalentPolicy> CurrentTalents;
    [SerializeField] ActiveTalentManager ActiveTalentManager;
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] Transform Player; 

    public List<TalentPolicy> GetUpgradableTalents(int numUpgrades)
    {
        List<TalentPolicy> result = new List<TalentPolicy>();

        List<TalentPolicy> randomSort = (List<TalentPolicy>)UtilityRandom.SortByRandom(CurrentTalents);

        foreach (TalentPolicy i in randomSort)
        {
            if (i.Upgradable)
            {
                result.Add(i.GetNextUpgrade());
                numUpgrades--;
                if (numUpgrades == 0)
                {
                    break;
                }
            }
        }

        return result; 
    }

    public void ApplyTalent(TalentPolicy t)
    {
        InjectionSet.InjectDependencies(t.transform);
        t.transform.parent = Player;
        t.transform.localPosition = Vector3.zero;
        t.ApplyPolicy();

        CurrentTalents.Add(t);
    }
}
