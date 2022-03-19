using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PossibleGameEvent : GameEvent
{
    public float Odds; 

    public PossibleGameEvent(SignalType sig, float f, Targetable t, float odds, int spreads, int recurs) : base(sig, f, t, spreads, recurs)
    {
        Odds = odds; 
    }
}
