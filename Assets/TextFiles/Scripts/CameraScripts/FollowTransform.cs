using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform Transform;
    [SerializeField] Vector3 Offset;
    [SerializeField] float Sensitivity;
    [SerializeField] bool useRegularUpdate = false; 

    float shakeLength = 0f;
    float shakeMagnitude = 0f;

    private float shakeStart = 0f; 

    public void ApplyShake(float magnitude, float length)
    {
        shakeLength = length;
        shakeMagnitude = magnitude;
        shakeStart = Time.realtimeSinceStartup; 
    }

    private void TrackPoint()
    {
        transform.position = Vector3.Lerp(transform.position, Transform.position + Offset + (Vector3)(shakeMagnitude * Random.insideUnitCircle), Sensitivity);
    }

    private void FixedUpdate()
    {
        if (!useRegularUpdate)
        {

            if (Time.realtimeSinceStartup - shakeStart > shakeLength)
            {
                shakeMagnitude = 0f;
            }

            TrackPoint();
        }
    }

    private void Update()
    {
        if (useRegularUpdate)
        {
            TrackPoint();
        }
    }
}
