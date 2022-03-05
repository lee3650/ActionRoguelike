using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Wielder : Targetable
{
    void OnHitLands(GameObject hit);
}
