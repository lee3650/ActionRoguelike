using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    [SerializeField] Rigidbody2D follow;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 Offset; 

    public void SetOffset(Vector2 offset)
    {
        Offset = offset;
    }

    private void FixedUpdate()
    {
        rb.position = follow.position + Offset;  
    }
}
