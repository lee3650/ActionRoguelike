using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Targetable : Entity
{
    Factions GetMyFaction();
    Vector2 GetMyPosition();
    bool IsAlive();
}
