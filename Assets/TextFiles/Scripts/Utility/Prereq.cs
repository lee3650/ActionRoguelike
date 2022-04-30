using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prereq : MonoBehaviour
{
    public static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new System.Exception("The given condition was not satisifed!");
        }
    }

    public static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new System.Exception("Condition not satisifed! Message: " + message);
        }
    }
}
