using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateOnDeath : MonoBehaviour, LateInitializable
{
    [SerializeField] State DeathState;
    [SerializeField] HealthManager HealthManager;
    [SerializeField] StateController StateController;

    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        StateController.EnterState(DeathState);
    }
}
