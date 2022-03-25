using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    [SerializeField] MovementController MovementController;
    [SerializeField] CurrentTarget CurrentTarget;
    [SerializeField] Rigidbody2D rb; 

    public void LookAtTarget()
    {
        if (CurrentTarget.HasTarget)
        {
            Vector2 delta = CurrentTarget.GetTargetPosition() - (Vector2)transform.position;
            float dir = UtilityFunctions.GetRotationFromDirection(delta.normalized);
            MovementController.PhysicallyRotateInDirection(dir);

        } else
        {
            float dir = UtilityFunctions.GetRotationFromDirection(rb.velocity.normalized);
            MovementController.PhysicallyRotateInDirection(dir);
        }
    }
}
