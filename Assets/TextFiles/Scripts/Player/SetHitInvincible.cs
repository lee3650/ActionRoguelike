using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHitInvincible : MonoBehaviour, SubEntity
{
    [SerializeField] float length = 0.5f;
    [SerializeField] MakeInvulnerable MakeInvulnerable;

    public void HandleEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case SignalType.Fire:
            case SignalType.Physical:
            case SignalType.Poison:
                MakeInvulnerable.StartTimedInvulnerability(length, true); 
                break; 
        }
    }
}
