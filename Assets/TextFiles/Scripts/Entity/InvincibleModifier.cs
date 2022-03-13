using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleModifier : MonoBehaviour, EventModifier
{
    public void ModifyEvents(List<GameEvent> events)
    {
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events.RemoveAt(i);
        }
    }
}
