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
    [SerializeField] HandAndArmGetter HandAndArm;

    [SerializeField] SendCollision Collider;

    [SerializeField] ReversedTracker ReversedTracker;

    private float timer = 0f;
    private float startRotation = 0f;
    private float startHandRotation = 0f;

    private int dir; 

    public override void EnterState()
    {
        Collider.StartColliding();
        timer = 0f;
        TrailRenderer.emitting = true;
        startRotation = HandAndArm.Arm.localEulerAngles.z;
        startHandRotation = HandAndArm.Hand.localEulerAngles.z;

        dir = ReversedTracker.Reversed ? -1 : 1; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / SwingLength;

        HandAndArm.Arm.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startRotation, startRotation + dir * SwingDistance, t, dir));
        HandAndArm.Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startHandRotation, startHandRotation + dir * WristDist, t, dir));

        if (timer > SwingLength)
        {
            StateController.EnterState(RecoveryState);
        }
    }

    public override void ExitState()
    {
        Collider.StopColliding();

        TrailRenderer.emitting = false; 
    }
}
