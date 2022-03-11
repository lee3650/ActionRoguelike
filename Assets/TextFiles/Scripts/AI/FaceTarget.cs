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
            float dir = KeyboardInput.GetRotationFromDirection(delta.normalized);
            MovementController.PhysicallyRotateInDirection(dir);

        } else
        {
            float dir = KeyboardInput.GetRotationFromDirection(rb.velocity.normalized);
            MovementController.PhysicallyRotateInDirection(dir);
        }
    }
}
