using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InitOnAwake : MonoBehaviour
{
    private void Awake()
    {
        foreach (Initializable i in GetComponents<Initializable>())
        {
            i.Init();
        }
    }
}
