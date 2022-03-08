using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAnticipation : State
{
    [SerializeField] protected float AnticipationLength;
    [Tooltip("Unit = degrees")]
    [SerializeField] protected float AnticipationDist;
    [Tooltip("Unit = degrees")]
    [SerializeField] protected float WristRotDist;

    [SerializeField] protected State NextState;

    [SerializeField] protected HandAndArmGetter HandAndArm;
    [SerializeField] protected ReversedTracker ReversedTracker;

    protected float timer = 0f;

    protected float adjustment = 0f;

    protected int dir;

    protected void SetupState()
    {
        adjustment = SwingAnimator.GetStart(ReversedTracker.Reversed);

        dir = SwingAnimator.GetStartDirection(ReversedTracker.Reversed);

        timer = 0f;

        print("next state: " + NextState);
    }

    protected void PartialUpdate()
    {
        timer += Time.fixedDeltaTime;

        SwingAnimator.AnimateSwing(HandAndArm, adjustment, adjustment, -WristRotDist, -AnticipationDist, AnticipationLength, timer, dir, dir);

        if (timer >= AnticipationLength)
        {
            StateController.EnterState(NextState);
        }
    }
}
