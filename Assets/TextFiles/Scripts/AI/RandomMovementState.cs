using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementState : State
{
    [SerializeField] DirectionalProjectileSender ProjectileSender;
    [SerializeField] CurrentTarget CurrentTarget;
    [SerializeField] AbstractMovementController MovementController;
    [SerializeField] State DefaultState;
    [SerializeField] Rigidbody2D rb;

    private Vector2 curDir;
    private bool inState = false; 

    public override void EnterState()
    {
        curDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        curDir.Normalize();
        inState = true;
        ProjectileSender.StartAttack();
    }

    public override void UpdateState()
    {
        if (CurrentTarget.HasTarget)
        {
            MovementController.MoveInDirection(curDir);
            print("moving in direction randomly! " + curDir);
        } else
        {
            StateController.EnterState(DefaultState);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (inState)
        {
            Vector2 normal = (Vector2)transform.position - collision.GetContact(0).point; 
            
            Vector2 projection = Vector3.Project(rb.velocity.normalized, -normal);
            
            Vector2 dif = rb.velocity.normalized - projection;
            
            Vector2 newDir = dif + normal;

            curDir = newDir.normalized; 
        }
    }

    public override void ExitState()
    {
        ProjectileSender.EndAttack();
        inState = false; 
    }
}
