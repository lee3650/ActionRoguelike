using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingState : State
{
    [SerializeField] float SwingLength;
    [SerializeField] float SwingDistance;
    [SerializeField] float WristDist;
    [SerializeField] RecoveryState RecoveryState;
    [SerializeField] TrailRenderer TrailRenderer;
    [SerializeField] Transform Hand; 

    private float timer = 0f;
    private float startRotation = 0f;
    private float startHandRotation = 0f;

    public override void EnterState()
    {
        timer = 0f;
        TrailRenderer.emitting = true;
        startRotation = transform.localEulerAngles.z;
        startHandRotation = Hand.localEulerAngles.z;
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / SwingLength;

        transform.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startRotation, startRotation + SwingDistance, t, 1));
        Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startHandRotation, startHandRotation + WristDist, t, 1));

        if (timer > SwingLength)
        {
            StateController.EnterState(RecoveryState);
        }
    }

    public override void ExitState()
    {
        TrailRenderer.emitting = false; 
    }
}
