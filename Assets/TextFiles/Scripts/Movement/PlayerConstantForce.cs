using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConstantForce : ConstantForceApplier
{
    [SerializeField] PlayerMovementController mc;

    public override void AddConstantForce(Vector2 force)
    {
        mc.AddToBaseVel(force);
    }

    public override void RemoveConstantForce(Vector2 force)
    {
        mc.ReduceBaseVel(force);
    }
}
