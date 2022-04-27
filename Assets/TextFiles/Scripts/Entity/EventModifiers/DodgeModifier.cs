using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeModifier : MonoBehaviour, EventModifier
{
    [SerializeField] float DodgeChance; 

    public void ModifyDodgeChance(float amt)
    {
        DodgeChance += amt; 
    }

    public void ModifyEvents(List<GameEvent> events)
    {
        if (UtilityRandom.PercentChance(DodgeChance))
        {
            for (int i = events.Count - 1; i >= 0; i--)
            {
                events.RemoveAt(i);
            }
        }
    }
}
