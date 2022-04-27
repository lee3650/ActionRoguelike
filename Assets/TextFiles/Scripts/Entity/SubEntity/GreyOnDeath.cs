using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyOnDeath : MonoBehaviour, SecondInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Color deathColor;

    public void SecondInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        print("Dead! Changing color!");
        sr.color = deathColor;
    }
}
