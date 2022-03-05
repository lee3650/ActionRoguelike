using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public SignalType Type;
    public float Magnitude;
    public Targetable Sender; 

    public GameEvent(SignalType myType, float magnitude, Targetable sender)
    {
        Type = myType;
        Magnitude = magnitude;
        Sender = sender;
    }

    public static GameEvent CopyEvent(GameEvent e)
    {
        return new GameEvent(e.Type, e.Magnitude, e.Sender);
    }
}
