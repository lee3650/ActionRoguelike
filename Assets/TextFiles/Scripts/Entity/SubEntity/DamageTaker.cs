using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour, SubEntity
{
    [SerializeField] HealthManager HealthManager;

    public void HandleEvent(GameEvent e)
    {
        if (e.HasStat(GameEvent.RepeatingKey))
        {
            //don't apply the first hit from a repeating GameEvent
            return; 
        }

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
