using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWielder : AbstractWielder
{
    public override void OnHitLands(GameObject hit)
    {
        print("hit " + hit.name);
    }
}
