using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : State
{
    [SerializeField] State AttackState;
    [SerializeField] State DefaultState;
    [SerializeField] CurrentTarget CurrentTarget;
    [SerializeField] MovementController MovementController;
    [SerializeField] float AttackRange; //duplication, fix later
    [SerializeField] FaceTarget FaceTarget;

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (CurrentTarget.HasTarget)
        {
            FaceTarget.LookAtTarget();

            Vector2 targetPos = CurrentTarget.GetTargetPosition();
            Vector2 delta = targetPos - (Vector2)transform.position;
            MovementController.MoveInDirection(delta.normalized);

            if (Vector2.Distance(targetPos, transform.position) < AttackRange)
            {
                StateController.EnterState(AttackState);
            }
        } else
        {
            StateController.EnterState(DefaultState);
        }
    }
    public override void ExitState()
    {

    }

}
