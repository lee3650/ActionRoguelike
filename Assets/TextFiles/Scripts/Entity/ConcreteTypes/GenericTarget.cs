using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTarget : MonoBehaviour, Targetable, Initializable
{
    [SerializeField] Factions Faction;
    [SerializeField] HealthManager HealthManager;

    SubEntity[] SubEntities;
    EventModifier[] EventModifiers; 

    public void Init()
    {
        SubEntities = GetComponents<SubEntity>();
        EventModifiers = GetComponents<EventModifier>();
        if (EventModifiers == null)
        {
            EventModifiers = new EventModifier[0];
        }
    }

    public Factions GetMyFaction()
    {
        return Faction;
    }

    public Vector2 GetMyPosition()
    {
        return transform.position; 
    }

    public void HandleEvent(GameEvent e)
    {
        List<GameEvent> eventList = new List<GameEvent>();

        eventList.Add(e);

        foreach (EventModifier em in EventModifiers)
        {
            em.ModifyEvents(eventList);
        }

        foreach (SubEntity se in SubEntities)
        {
            foreach (GameEvent ge in eventList)
            {
                se.HandleEvent(ge);
            }
        }
    }

    public bool IsAlive()
    {
        return HealthManager.IsAlive(); 
    }
}
