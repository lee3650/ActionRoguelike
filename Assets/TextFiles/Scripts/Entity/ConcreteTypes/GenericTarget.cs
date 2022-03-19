using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTarget : MonoBehaviour, Targetable, Initializable
{
    [SerializeField] Factions Faction;
    [SerializeField] HealthManager HealthManager;

    List<SubEntity> SubEntities;
    List<EventModifier> EventModifiers; 

    public void Init()
    {
        SubEntities = new List<SubEntity>();
        SubEntities.AddRange(GetComponents<SubEntity>());
        EventModifiers = new List<EventModifier>();
        EventModifiers.AddRange(GetComponents<EventModifier>());
    }

    public void AddEventModifier(EventModifier modifier)
    {
        EventModifiers.Insert(0, modifier);
    }

    public void RemoveEventModifier(EventModifier modifier)
    {
        EventModifiers.Remove(modifier);
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
        if (e is PossibleGameEvent)
        {
            if (!UtilityRandom.PercentChance((e as PossibleGameEvent).Odds))
            {
                return; 
            }
            e = GameEvent.ConvertToGameEvent(e as PossibleGameEvent);
        }

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
