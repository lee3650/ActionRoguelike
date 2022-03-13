using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySetting : MonoBehaviour, LateInitializable
{
    [SerializeField] bool enableSpray = true;

    public static bool EnableSpray
    {
        get;
        private set; 
    }

    public void LateInit()
    {
        EnableSpray = enableSpray; 
    }
}
