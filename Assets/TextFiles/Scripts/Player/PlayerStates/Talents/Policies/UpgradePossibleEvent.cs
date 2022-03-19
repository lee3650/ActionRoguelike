using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePossibleEvent : TalentPolicy
{
    [SerializeField] AddPossibleEvent AddPossibleEvent;
    [SerializeField] float damageMultiplier = 1;
    [SerializeField] float oddsMultiplier = 1;
    [SerializeField] int AddSpreads = 0;
    [SerializeField] int AddRecurs = 0;

    public override void ApplyPolicy()
    {
        AddPossibleEvent.MultiplyEventChances(oddsMultiplier);
        AddPossibleEvent.MultiplyEventDamage(damageMultiplier);
        AddPossibleEvent.AddSpreads(AddSpreads);
        AddPossibleEvent.AddRecurs(AddRecurs);
    }
}
