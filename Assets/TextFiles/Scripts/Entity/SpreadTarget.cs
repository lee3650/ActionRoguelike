using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpreadTarget : MonoBehaviour
{
    [SerializeField] Factions MyFaction; 
    public abstract bool CanReceiveSpread(SignalType type);
    public abstract void ReceiveSpread(GameEvent e);

    public Vector2 GetPosition()
    {
        return transform.position; 
    }

    public Factions GetMyFaction()
    {
        return MyFaction;
    }
}
