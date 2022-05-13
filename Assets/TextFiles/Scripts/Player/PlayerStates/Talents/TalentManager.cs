using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    [SerializeField] private List<TalentPolicy> CurrentTalents;
    [SerializeField] ActiveTalentManager ActiveTalentManager;
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] Transform Player; 

    public List<TalentPolicy> GetCurrentTalents()
    {
        return CurrentTalents; 
    }

    public void ApplyTalent(TalentPolicy t)
    {
        InjectionSet.InjectDependencies(t.transform);
        t.transform.parent = Player;
        t.transform.localPosition = Vector3.zero;
        t.ApplyPolicy();

        if (t.IsUpgrade)
        {
            t.Parent.AppliedUpgrade(t);
        }

        CurrentTalents.Add(t);
    }

    public void RemoveTalent(TalentPolicy t)
    {
        t.transform.parent = null;
        CurrentTalents.Remove(t);
    }
}
