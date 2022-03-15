using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallDamageUpgrade : TalentPolicy
{
    [SerializeField] PlayerRecallState PlayerRecallState;
    public override void ApplyPolicy()
    {
        PlayerRecallState.EnableDamage();
    }
}
