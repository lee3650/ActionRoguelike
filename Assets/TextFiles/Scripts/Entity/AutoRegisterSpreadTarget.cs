using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRegisterSpreadTarget : MonoBehaviour, LateInitializable
{
    [SerializeField] SpreadTarget MyTarget;
    [SerializeField] HealthManager HealthManager;
    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
        SpreadManager.RegisterSpreadTarget(MyTarget);
    }

    private void OnDeath()
    {
        SpreadManager.RemoveTarget(MyTarget);
    }
}
