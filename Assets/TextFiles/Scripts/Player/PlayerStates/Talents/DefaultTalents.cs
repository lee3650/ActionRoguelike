using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTalents : MonoBehaviour, LateInitializable
{
    [SerializeField] List<TalentPolicy> StartingTalents;
    [SerializeField] TalentManager TalentManager;

    public void LateInit()
    {
        foreach (TalentPolicy t in StartingTalents)
        {
            TalentPolicy instance = Instantiate(t);
            TalentManager.ApplyTalent(instance);
        }
    }
}
