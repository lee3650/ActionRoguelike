using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : State
{
    [SerializeField] float RecoveryLength;
    [SerializeField] Transform Hand;
    [SerializeField] [Range(-1, 1)] int WristDir;
    [SerializeField] PlayerWeaponDefaultState defaultState;
    [SerializeField] MeleeWeapon MyWeapon;

    [SerializeField] bool toggleReverse;
    [SerializeField] bool toggleEnd; 

    [SerializeField] ReversedTracker ReversedTracker;

    private float timer = 0f;
    private float startRotation = 0f;
    private float startHandRotation = 0f;

    int dir;
    int w_dir;
    float end; 

    public override void EnterState()
    {
        timer = 0f;
        startRotation = transform.localEulerAngles.z;
        startHandRotation = Hand.localEulerAngles.z;
        dir = ReversedTracker.Reversed ? 1 : -1;
        w_dir = ReversedTracker.Reversed ? -WristDir : WristDir;

        if (toggleEnd)
        {
            end = ReversedTracker.Reversed ? 0 : 180;
        } else
        {
            end = 0f;
        }

    }
    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / RecoveryLength; 

        transform.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startRotation, end, t, dir));
        Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startHandRotation, end, t, w_dir));

        if (timer >= RecoveryLength)
        {
            StateController.EnterState(defaultState);
        }
    }

    public override void ExitState()
    {
        MyWeapon.FinishedAttack();
        if (toggleReverse)
        {
            ReversedTracker.ToggleReversed();
        }
    }
}
