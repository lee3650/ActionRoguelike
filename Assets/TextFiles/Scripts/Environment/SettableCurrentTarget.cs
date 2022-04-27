using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettableCurrentTarget : AbstractCurrentTarget
{
    public void SetCurrentTarget(Targetable t)
    {
        ClosestTarget = t; 
    }
}
