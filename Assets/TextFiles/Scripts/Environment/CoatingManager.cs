using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatingManager : MonoBehaviour, EventModifier
{
    //so, it'll probably just delegate this to the list of events it currently has. 

    private List<EventModifier> Coatings; 

    public void ModifyEvents(List<GameEvent> events)
    {

    }
}
