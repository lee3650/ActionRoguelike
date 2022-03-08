using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowState : State, Dependency<Transform>
{
    [SerializeField] private SendCollision Collider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float throwSpeed;
    [SerializeField] float spinSpeed;
    [SerializeField] TrailRenderer TrailRenderer;
    [SerializeField] bool enableTrail; 

    private Transform wielder; 

    public void InjectDependency(Transform t)
    {
        wielder = t; 
    }

    public override void EnterState()
    {
        Collider.StartColliding();

        //unparent ourselves, I guess.
        transform.parent = null;
        rb.isKinematic = false;
        rb.velocity = wielder.right * throwSpeed;
        rb.angularVelocity = spinSpeed;

        if (enableTrail)
        {
            TrailRenderer.emitting = true; 
        } 
    }

    public override void UpdateState()
    {
        //we need to somehow stop when we hit something 
        //and drop ourselves. 
    }

    public override void ExitState()
    {
        if (enableTrail)
        {
            TrailRenderer.emitting = false;
        }
        Collider.StopColliding();
    }
}
