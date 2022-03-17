using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSwing : State
{
    [SerializeField] protected float SwingLength;
    [SerializeField] protected float SwingDistance;
    [SerializeField] protected float WristDist;
    [SerializeField] protected State NextState;
    [SerializeField] protected HandAndArmGetter HandAndArm;

    [SerializeField] protected ReversedTracker ReversedTracker;

    protected float timer = 0f;
    private float startRotation = 0f;
    private float startHandRotation = 0f;

    private int dir;

    protected void SetupState()
    {
        timer = 0f;
        startRotation = HandAndArm.GetArmRotation();
        startHandRotation = HandAndArm.GetHandRotation();

        dir = -SwingAnimator.GetStartDirection(ReversedTracker.Reversed); // ReversedTracker.Reversed ? -1 : 1; 
    }

    protected void PartialUpdate()
    {
        timer += Time.fixedDeltaTime;

        SwingAnimator.AnimateSwing(HandAndArm, startRotation, startHandRotation, WristDist, SwingDistance, SwingLength, timer, dir, dir);

        if (timer > SwingLength)
        {
            StateController.EnterState(NextState);
        }
    }
}
