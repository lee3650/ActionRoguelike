using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOnDeath : MonoBehaviour, LateInitializable
{
    [SerializeField] float XPGain;
    [SerializeField] HealthManager hm; 

    public void LateInit()
    {
        hm.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        XPManager.AddXP(XPGain);
    }
}
