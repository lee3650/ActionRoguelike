using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedInit
{
    public static void PerformInitialization(Transform t)
    {
        InitializeInit(t);

        InitializeSecond(t);

        InitializeLate(t);
    }

    public static void PerformInitialization(Transform[] ts)
    {
        foreach (Transform t in ts)
        {
            InitializeInit(t);
        }

        foreach (Transform t in ts)
        {
            InitializeSecond(t);
        }

        foreach (Transform t in ts)
        {
            InitializeLate(t);
        }
    }

    public static void InitializeInit(Transform t)
    {
        Initializable[] first = t.GetComponents<Initializable>();
        foreach (Initializable i in first)
        {
            i.Init();
        }
    }

    public static void InitializeSecond(Transform t)
    {
        SecondInitializable[] seconds = t.GetComponents<SecondInitializable>();
        foreach (SecondInitializable si in seconds)
        {
            si.SecondInit();
        }
    }

    public static void InitializeLate(Transform t)
    {
        LateInitializable[] lasts = t.GetComponents<LateInitializable>();
        foreach (LateInitializable li in lasts)
        {
            li.LateInit();
        }
    }
}
