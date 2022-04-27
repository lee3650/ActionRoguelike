using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbConstantForce : ConstantForceApplier
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float forceMultiplier; 
    private Vector2 baseVel = new Vector2(0, 0); 

    public override void AddConstantForce(Vector2 force)
    {
        baseVel += force; 
    }

    public override void RemoveConstantForce(Vector2 force)
    {
        baseVel -= force; 
    }

    private void FixedUpdate()
    {
        rb.AddForce(baseVel * forceMultiplier); 
    }
}
