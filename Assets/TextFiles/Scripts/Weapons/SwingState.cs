using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingState : State
{
    [SerializeField] float AnticipationLength;
    [SerializeField] float SwingLength;
    [SerializeField] float RecoveryLength;
    [Tooltip("Units = degrees")]
    [SerializeField] float AnticipationDistance;
    [Tooltip("Units = degrees")]
    [SerializeField] float SwingDist;
    [SerializeField] Transform WeaponParent;
    [SerializeField] MeleeWeapon MeleeWeapon;
    [SerializeField] PlayerWeaponDefaultState DefaultState;
    [SerializeField] MovementController MovementController;
    [SerializeField] TrailRenderer TrailRenderer;

    private float timer = 0f;
    private float startRotation = 0f;
    float elapsedTime = 0f; 

    public override void EnterState()
    {
        timer = AnticipationLength + SwingLength + RecoveryLength;
        startRotation = MovementController.GetRotation();
        elapsedTime = 0f; 
    }

    private bool InAnticipation(float timer)
    {
        return timer > SwingLength + RecoveryLength; 
    }

    private bool InSwing(float timer)
    {
        return timer > SwingLength && !InAnticipation(timer);
    }

    private bool InRecovery(float timer)
    {
        return timer < RecoveryLength; 
    }

    public override void UpdateState()
    {
        timer -= Time.fixedDeltaTime;

        elapsedTime += Time.fixedDeltaTime;

        if (timer < 0f)
        {
            StateController.EnterState(DefaultState);
        }

        if (InAnticipation(timer))
        {
            float t = elapsedTime / AnticipationLength;

            WeaponParent.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(0, -120, t));

            MovementController.PhysicallyRotateInDirection(Mathf.LerpAngle(startRotation, startRotation + AnticipationDistance, t));
        }
        if (InSwing(timer))
        {
            //Well, this is officially a mess

            if (!TrailRenderer.emitting)
            {
                TrailRenderer.emitting = true; 
            }

            float t = (elapsedTime - AnticipationLength) / SwingLength;

            float startAngle = startRotation + AnticipationDistance;

            WeaponParent.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(-120, -60, t));

            MovementController.PhysicallyRotateInDirection(Mathf.LerpAngle(startAngle, startAngle + SwingDist, t));
        }
        if (InRecovery(timer))
        {
            if (TrailRenderer.emitting)
            {
                TrailRenderer.emitting = false;
            }

            float totalTime = (elapsedTime - AnticipationLength - SwingLength);
            float t = totalTime / RecoveryLength;

            print("recovery t: " + t);
            print("total time: " + totalTime);

            float startAngle = startRotation + AnticipationDistance + SwingDist;

            WeaponParent.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(-60, 0, t));

            MovementController.PhysicallyRotateInDirection(Mathf.LerpAngle(startAngle, startRotation, t));
        }
    }

    public override void ExitState()
    {
        MeleeWeapon.FinishedAttack();
    }
}
