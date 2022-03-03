using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform Transform;
    [SerializeField] Vector3 Offset;
    [SerializeField] float Sensitivity;

    private void TrackPoint()
    {
        transform.position = Vector3.Lerp(transform.position, Transform.position + Offset, Sensitivity);
    }

    private void FixedUpdate()
    {
        TrackPoint();
    }
}
