using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    [SerializeField] CurrentTarget CurrentTarget;
    [SerializeField] State PursuitState;

    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        if (CurrentTarget.HasTarget)
        {
            StateController.EnterState(PursuitState);
        }
    }
    public override void ExitState()
    {

    }
}
