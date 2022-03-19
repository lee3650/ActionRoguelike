using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFreeze : MonoBehaviour, SubEntity, Dependency<StateController>
{
    [SerializeField] FreezeState FreezeState;
    [SerializeField] HealthManager HealthManager;

    public void InjectDependency(StateController sc)
    {
        StateController = sc; 
    }

    private StateController StateController;

    public void HandleEvent(GameEvent e)
    {
        if (e.Type == SignalType.Freeze)
        {
            if (HealthManager.IsAlive())
            {
                FreezeState.FreezeLength = e.Magnitude; 
                StateController.EnterState(FreezeState);
            }
        }
    }
}
