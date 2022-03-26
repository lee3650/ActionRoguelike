using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, Initializable
{
    [SerializeField] Rigidbody2D rb;
    //later we can do a move speed supplier in order to have stats and effects change this 
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float RotateSpeed = 10f;

    float effectiveSpeed; 

    public void Init() 
    {
        effectiveSpeed = baseMoveSpeed; 
    }

    public void SetRotation(float dir)
    {
        rb.rotation = dir; 
    }

    public void ModifyMoveSpeed(float modification)
    {
        effectiveSpeed *= modification;
    }

    public void ResetMoveSpeed()
    {
        effectiveSpeed = baseMoveSpeed; 
    }

    public void MoveInDirection(Vector2 dir)
    {
        rb.AddForce(dir * effectiveSpeed);
    }

    public void AddForce(float force, Vector2 dir)
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
