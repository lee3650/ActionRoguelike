using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateInitOnStart : MonoBehaviour
{
    void Start()
    {
        foreach (LateInitializable i in GetComponents<LateInitializable>())
        {
            i.LateInit();
        }
    }
}
