using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubEntity : MonoBehaviour, SubEntity
{
    public List<GameEvent> handledEvents = new List<GameEvent>();
    public void HandleEvent(GameEvent e)
    {
        handledEvents.Add(e);
    }
}
