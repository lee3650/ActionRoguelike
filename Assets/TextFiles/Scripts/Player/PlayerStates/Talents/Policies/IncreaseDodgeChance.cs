using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDodgeChance : TalentPolicy
{
    [SerializeField] float increaseAmt;
    [SerializeField] DodgeModifier modifier;

    public override void ApplyPolicy()
    {
        modifier.ModifyDodgeChance(increaseAmt);
    }
}
