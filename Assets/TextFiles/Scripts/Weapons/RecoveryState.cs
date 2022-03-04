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

    private float timer = 0f;
    private float startRotation = 0f;
    private float startHandRotation = 0f;

    public override void EnterState()
    {
        timer = 0f;
        startRotation = transform.localEulerAngles.z;
        startHandRotation = Hand.localEulerAngles.z;
    }
    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        float t = timer / RecoveryLength; 

        transform.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startRotation, 0, t, -1));
        Hand.localEulerAngles = new Vector3(0f, 0f, UtilityFunctions.LerpAngleDirection(startHandRotation, 0, t, WristDir));

        if (timer >= RecoveryLength)
        {
            StateController.EnterState(defaultState);
        }
    }
    public override void ExitState()
    {
        MyWeapon.FinishedAttack();
    }
}
