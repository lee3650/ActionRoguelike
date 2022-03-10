using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWielder : GenericTarget, Wielder
{
    public abstract void OnHitLands(GameObject hit);
}
