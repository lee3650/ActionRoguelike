using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public SignalType Type;
    public float Magnitude;
    public Targetable Sender;
    public int Spreads = 0;
    public int Recurs = 0; 

    public GameEvent(SignalType myType, float magnitude, Targetable sender)
    {
        Type = myType;
        Magnitude = magnitude;
        Sender = sender;
        Spreads = 0;
        Recurs = 0; 
    }

    public GameEvent(SignalType myType, float magnitude, Targetable sender, int spreads, int recurs) : this(myType, magnitude, sender)
    {
        Spreads = spreads;
        Recurs = recurs; 
    }

    public static GameEvent ConvertToGameEvent(PossibleGameEvent possible)
    {
        return new GameEvent(possible.Type, possible.Magnitude, possible.Sender, possible.Spreads, possible.Recurs); 
    }

    public static GameEvent CopyEvent(GameEvent e)
    {
        PossibleGameEvent pg = e as PossibleGameEvent; 
        if (pg != null)
        {
            return new PossibleGameEvent(e.Type, e.Magnitude, e.Sender, pg.Odds, pg.Spreads, pg.Recurs); 
        }
        return new GameEvent(e.Type, e.Magnitude, e.Sender, e.Spreads, e.Recurs);
    }
}
