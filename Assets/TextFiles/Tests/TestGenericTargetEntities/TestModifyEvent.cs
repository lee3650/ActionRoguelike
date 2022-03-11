using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModifyEvent : MonoBehaviour, EventModifier
{
    public List<GameEvent> addedEvents = new List<GameEvent>()
    {
        (new GameEvent(SignalType.Knockback, 0, null)),
        (new GameEvent(SignalType.Knockback, 0, null)),
        (new GameEvent(SignalType.Knockback, 0, null)),
    };

    public void ModifyEvents(List<GameEvent> events)
    {
        events.AddRange(addedEvents);
    }
}
