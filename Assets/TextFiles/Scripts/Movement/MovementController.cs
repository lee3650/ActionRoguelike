using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : AbstractMovementController, LateInitializable, StatListener
{
    [SerializeField] Rigidbody2D rb;
    //later we can do a move speed supplier in order to have stats and effects change this 
    [SerializeField] float RotateSpeed = 10f;
    [SerializeField] StatsList StatsList;

    public void LateInit() 
    {
        baseMoveSpeed = StatsList.GetStat(speedStat);
        effectiveMoveSpeed = baseMoveSpeed;
        StatsList.RegisterListener(speedStat, this);
    }

    public void StatChanged(string stat, float newVal)
    {
        effectiveMoveSpeed = newVal;
        baseMoveSpeed = newVal;
    }

    public void SetRotation(float dir)
    {
        rb.rotation = dir; 
    }

    public override void MoveInDirection(Vector2 dir)
    {
        //rb.velocity = dir * effectiveSpeed; 
        rb.AddForce(dir * effectiveMoveSpeed);
    }

    public override void AddForce(float force, Vector2 dir)
    {
        rb.AddForce(force * dir);
    }

    public void PhysicallyRotateInDirection(float dir)
    {
        rb.rotation = Mathf.LerpAngle(rb.rotation, dir, RotateSpeed);
    }

    public float GetRotation()
    {
        return rb.rotation; 
    }
}
