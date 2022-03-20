using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractRecovery : State
{
    [SerializeField] protected float RecoveryLength;
    [SerializeField] protected HandAndArmGetter HandAndArm;
    [SerializeField] [Range(-1, 1)] protected int WristDir;
    [SerializeField] protected State nextState;
    [SerializeField] protected ReversedTracker ReversedTracker;
    protected float timer = 0f;

    protected float startRotation = 0f;
    protected float startHandRotation = 0f;

    protected int dir;
    protected int w_dir;
    protected float end;

    protected void SetupState()
    {
        timer = 0f;
        startRotation = HandAndArm.GetArmRotation();
        startHandRotation = HandAndArm.GetHandRotation();
        dir = ReversedTracker.Reversed ? 1 : -1;
        w_dir = ReversedTracker.Reversed ? -WristDir : WristDir;

        end = ReversedTracker.Reversed ? 0 : 180;
    }

    protected void PartialUpdate()
    {
        timer += Time.fixedDeltaTime;

        SwingAnimator.AnimateRecovery(HandAndArm, startRotation, startHandRotation, end, RecoveryLength, timer, dir, w_dir);

        if (timer >= RecoveryLength)
        {
            StateController.EnterState(nextState);
        }
    }

    protected void PartialExitState()
    {
        ReversedTracker.ToggleReversed();
    }
}
