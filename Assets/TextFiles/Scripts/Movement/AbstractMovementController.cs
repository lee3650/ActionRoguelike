using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovementController : MonoBehaviour
{
    public const string speedStat = "speed";

    public abstract void MoveInDirection(Vector2 dir);
    public abstract void AddForce(float force, Vector2 dir);

    public void ResetMoveSpeed()
    {
        effectiveMoveSpeed = baseMoveSpeed; 
    }
    public void ModifyMoveSpeed(float modifier)
    {
        effectiveMoveSpeed *= modifier;
    }

    protected float baseMoveSpeed;
    protected float effectiveMoveSpeed; 
}
