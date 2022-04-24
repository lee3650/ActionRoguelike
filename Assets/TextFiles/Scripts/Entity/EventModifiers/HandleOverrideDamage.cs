using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOverrideDamage : MonoBehaviour, EventModifier
{
    public void ModifyEvents(List<GameEvent> gameEvents)
    {
        foreach (GameEvent e in gameEvents)
        {
            if (e.HasStat(GameEvent.OverrideDamageKey))
            {
                e.Magnitude = e.GetStatAsFloat(GameEvent.OverrideDamageKey, 0f);
                e.RemoveStat(GameEvent.OverrideDamageKey);
            }
        }
    }
}
