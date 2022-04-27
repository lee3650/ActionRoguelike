using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPursuitState : State
{
    [SerializeField] State DefaultState; 
    [SerializeField] State[] Attacks;
    [SerializeField] MovementController MovementController;
    [SerializeField] float[] Cooldowns;
    [SerializeField] FaceTarget FaceTarget;

    [SerializeField] float pursuitRadius; 

    [SerializeField] CurrentTarget CurrentTarget;

    float timer = 0f;

    private float currentCooldown; 

    public override void EnterState()
    {
        timer = 0f;
        currentCooldown = Cooldowns[Random.Range(0, Cooldowns.Length)];
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        if (CurrentTarget.HasTarget)
        {
            if (timer > currentCooldown)
            {
                int next = Random.Range(0, Attacks.Length);
                currentCooldown = Cooldowns[next];
                StateController.EnterState(Attacks[next]);
                return;
            }

            if (Vector2.Distance(transform.position, CurrentTarget.GetTargetPosition()) > pursuitRadius)
            {
                Vector2 delta = CurrentTarget.GetTargetPosition() - (Vector2)transform.position;
                MovementController.MoveInDirection(delta.normalized);
            }

            FaceTarget.LookAtTarget();
        }
        else
        {
            StateController.EnterState(DefaultState);
        }
    }

    public override void ExitState()
    {

    }
}
