using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventModifier
{
    void ModifyEvents(List<GameEvent> events);
}
