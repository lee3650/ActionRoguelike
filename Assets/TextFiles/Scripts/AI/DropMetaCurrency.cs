using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMetaCurrency : MonoBehaviour, Initializable
{
    [SerializeField] float dropChance = 25f;
    [SerializeField] int dropAmt = 1;

    private HealthManager HealthManager;

    public void Init()
    {
        HealthManager = GetComponent<HealthManager>();
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        print("currency on death!");
        if (UtilityRandom.PercentChance(dropChance))
        {
            print("dropping currency!");
            MetaCurrencyManager.Balance += dropAmt; 
        }
    }
}
