using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager hm;

    public void LateInit()
    {
        hm.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
