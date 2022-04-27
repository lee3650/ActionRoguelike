using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOffset : MonoBehaviour
{
    [SerializeField] Transform Transform;
    [SerializeField] Vector2 offset;

    private void Update()
    {
        transform.position = (Vector2)Transform.position + offset;
    }
}
