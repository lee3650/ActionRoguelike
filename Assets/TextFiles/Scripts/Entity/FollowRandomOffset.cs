using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRandomOffset : MonoBehaviour, Dependency<Rigidbody2D>, LateInitializable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Rigidbody2D follow; 
    [SerializeField] Vector2 Offset;
    [SerializeField] float stiffness;
    [SerializeField] float FollowRadius; 

    public void LateInit()
    {
        Offset = Random.insideUnitCircle;
        Offset.Normalize();
        Offset *= FollowRadius;
    }

    public void InjectDependency(Rigidbody2D f)
    {
        follow = f;
    }

    void FixedUpdate()
    {
        rb.position = Vector2.Lerp(rb.position, Offset + follow.position, stiffness);
    }
}
