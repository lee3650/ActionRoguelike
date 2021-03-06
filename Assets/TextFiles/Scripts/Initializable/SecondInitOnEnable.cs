using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SecondInitOnEnable : MonoBehaviour
{
    public void OnEnable()
    {
        foreach (SecondInitializable i in GetComponents<SecondInitializable>())
        {
            i.SecondInit();
        }    
    }
}
