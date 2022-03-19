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
            case SignalType.Fire:
            case SignalType.Poison: 
                HealthManager.TakeDamage(e.Magnitude);
                break;
        }
    }
}
