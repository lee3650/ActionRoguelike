using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //later we can do a move speed supplier in order to have stats and effects change this 
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float RotateSpeed = 10f; 

    public void MoveInDirection(Vector2 dir)
    {
        rb.AddForce(dir * baseMoveSpeed);
    }

    public void AddForce(float force, Vector2 dir)
    {
        rb.AddForce(force * dir);
    }

    public void RotateInDirection(float dir)
    {
        rb.rotation = Mathf.LerpAngle(rb.rotation, dir, RotateSpeed);
    }
}
