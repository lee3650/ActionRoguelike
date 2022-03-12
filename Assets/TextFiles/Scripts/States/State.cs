using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateController StateController;
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public void SetStateController(StateController controller)
    {
        StateController = controller; 
    }
}