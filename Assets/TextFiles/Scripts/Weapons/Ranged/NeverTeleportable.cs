using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverTeleportable : Teleportable
{
    public override bool CanTeleport()
    {
        return false; 
    }
}
