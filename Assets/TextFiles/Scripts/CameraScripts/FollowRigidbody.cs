using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    [SerializeField] Rigidbody2D follow;
    [SerializeField] Rigidbody2D rb;
    
    private void Update()
    {
        rb.position = follow.position;  
    }
}
