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
    [SerializeField] Transform Hand;

    [SerializeField] SwingState SwingState;

    private float timer = 0f;

    public override void EnterState()
    {
        timer = 0f; 
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / AnticipationLength;

        //move ourselves
        transform.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(0f, AnticipationDist, t, -1));

        //move the weapon sprite/collider
        Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(0f, WristRotDist, t, -1));

        if (timer >= AnticipationLength)
        {
            StateController.EnterState(SwingState);
        }
    }

    public override void ExitState()
    {

    }
}
