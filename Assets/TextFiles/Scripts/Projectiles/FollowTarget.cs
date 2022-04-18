using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour, Dependency<CurrentTarget>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float velocity; 

    private Targetable target; 

    public void InjectDependency(CurrentTarget t)
    {
        target = t.ClosestTarget;
    }

    private void FixedUpdate()
    {
        rb.velocity = (target.GetMyPosition() - (Vector2)transform.position).normalized * velocity;
    }
}
