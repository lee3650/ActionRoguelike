using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedInit
{
    public static void PerformInitialization(Transform t)
    {
        Initializable[] first = t.GetComponents<Initializable>();
        foreach (Initializable i in first)
        {
            i.Init();
        }

        SecondInitializable[] seconds = t.GetComponents<SecondInitializable>();
        foreach (SecondInitializable si in seconds)
        {
            si.SecondInit();
        }

        LateInitializable[] lasts = t.GetComponents<LateInitializable>();
        foreach (LateInitializable li in lasts)
        {
            li.LateInit();
        }
    }
}
