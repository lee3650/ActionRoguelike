using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour, Dependency<StateController>
{
    protected StateController StateController;
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public void InjectDependency(StateController controller)
    {
        print("got state controller " + controller);
        StateController = controller; 
    }
}