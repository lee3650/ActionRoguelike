using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    [SerializeField] Rigidbody2D follow;
    [SerializeField] Rigidbody2D rb;
    
    private void FixedUpdate()
    {
        rb.position = follow.position;  
    }
}
