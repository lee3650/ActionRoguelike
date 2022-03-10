using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour, SubEntity
{
    [SerializeField] HealthManager HealthManager;

    public void HandleEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case SignalType.Physical:
                HealthManager.TakeDamage(e.Magnitude);
                break;
        }
    }
}
