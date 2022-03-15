using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealAbility : TalentPolicy
{
    [SerializeField] PlayerHealState PlayerHealState;
    [SerializeField] float LengthMultiplier = 1f;
    [Tooltip("Added to heal amount")]
    [SerializeField] float AmtModifier = 0f;
    [SerializeField] float MoveMultiplier = 1; 

    public override void ApplyPolicy()
    {
        PlayerHealState.ModifyHealLength(LengthMultiplier);
        PlayerHealState.ModifyHealAmt(AmtModifier);
        PlayerHealState.ModifyMoveSpeedInHeal(MoveMultiplier);
    }
}
