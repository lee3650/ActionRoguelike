using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeState : State
{
    public float FreezeLength;

    private float timer = 0f;

    [SerializeField] State DefaultState;

    public override void EnterState()
    {
        timer = 0f; 
    }
    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime; 
        if (timer > FreezeLength)
        {
            StateController.EnterState(DefaultState);
        }
    }
    public override void ExitState()
    {

    }
}
