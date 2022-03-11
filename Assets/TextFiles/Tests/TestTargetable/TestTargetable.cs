using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetable : Targetable
{
    public Factions faction;
    public bool isAlive;
    public Vector2 myPosition; 

    public TestTargetable(Factions f, bool alive, Vector2 pos)
    {
        faction = f;
        isAlive = alive;
        myPosition = pos; 
    }

    public void HandleEvent(GameEvent e)
    {

    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public Factions GetMyFaction()
    {
        return faction;
    }
    public Vector2 GetMyPosition()
    {
        return myPosition; 
    }
}
