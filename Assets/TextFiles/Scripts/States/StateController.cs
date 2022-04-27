using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour, LateInitializable
{
    [SerializeField] Component InitialState;

    private List<State> StateHistory = new List<State>();

    private State CurrentState;

    private bool initialized = false; 

    public void LateInit()
    {
        initialized = true; 
        EnterState(InitialState as State); 
    }

    public void EnterState(State newState)
    {
        if (newState == null)
        {
            throw new System.Exception("The entered state was null from state " + CurrentState);
        }

        CurrentState?.ExitState();

        CurrentState = newState;

        StateHistory.Add(CurrentState);
        if (StateHistory.Count > 10)
        {
            StateHistory.RemoveAt(0);
        }

        CurrentState.EnterState();
    }

    void FixedUpdate()
    {
        if (initialized)
        {
            CurrentState.UpdateState();
        }
    }
}
