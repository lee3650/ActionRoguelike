using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnticipationState : State
{
    [SerializeField] float AnticipationLength; 
    [Tooltip("Unit = degrees")]
    [SerializeField] float AnticipationDist;
    [Tooltip("Unit = degrees")]
    [SerializeField] float WristRotDist;
    [SerializeField] HandAndArmGetter HandAndArm;

    [SerializeField] SwingState SwingState;

    [SerializeField] ReversedTracker ReversedTracker;

    private float timer = 0f;

    private float adjustment = 0f;

    private float startRot = 0f;

    private int dir; 

    public override void EnterState()
    {
        if (ReversedTracker.Reversed)
        {
            adjustment = 180f;
        } else
        {
            adjustment = 0; 
        }

        dir = ReversedTracker.Reversed ? 1 : -1;

        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / AnticipationLength;


        HandAndArm.Arm.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(adjustment, -dir * AnticipationDist + adjustment, t, dir));

        HandAndArm.Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(adjustment, -dir * WristRotDist + adjustment, t, dir));

        if (timer >= AnticipationLength)
        {
            StateController.EnterState(SwingState);
        }
    }

    public override void ExitState()
    {

    }
}
