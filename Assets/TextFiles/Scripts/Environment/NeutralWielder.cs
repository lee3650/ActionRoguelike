using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralWielder : MonoBehaviour, Targetable
{
    public Factions GetMyFaction()
    {
        return Factions.Environment;
    }

    public Vector2 GetMyPosition()
    {
        return transform.position;
    }

    public bool IsAlive()
    {
        return false; //?
    }

    public void HandleEvent(GameEvent e)
    {
        //do nothing? 
    }
}
