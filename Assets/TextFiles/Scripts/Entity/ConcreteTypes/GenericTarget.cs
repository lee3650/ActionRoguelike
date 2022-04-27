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
        if (e.HasStat(GameEvent.OddsKey))
        {
            print("Game Event " + e.Type + " had stat odds key; odds were " + e.GetStatAsFloat(GameEvent.OddsKey, 100));
            if (!UtilityRandom.PercentChance(e.GetStatAsFloat(GameEvent.OddsKey, 100)))
            {
                return; 
            }
            e.RemoveStat(GameEvent.OddsKey);
        }
        else
        {
            print("Game Event " + e.Type + " did not have stats odds key");
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
