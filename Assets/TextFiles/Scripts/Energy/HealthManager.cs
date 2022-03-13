using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthManager : MonoBehaviour, Initializable
{
    public event Action HealthChanged = delegate { };
    public event Action DamageTaken = delegate { };
    public event Action OnDeath = delegate { };

    [SerializeField] float MaxHealth;

    [SerializeField] private float currentHealth;

    public float GetHealthPercentage()
    {
        return currentHealth / MaxHealth; 
    }

    public void Init()
    {
        currentHealth = MaxHealth; 
    }

    public void Heal(float amt)
    {
        currentHealth += amt; 
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth; 
        }
        HealthChanged();
    }

    public void TakeDamage(float amt)
    {
        currentHealth -= amt;
        DamageTaken();
        HealthChanged();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath();
        }
    }

    public void IncreaseMaxHealth(float amt)
    {
        MaxHealth += amt;
        Heal(amt);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    /// <summary>
    /// Test method. Not for production use.
    /// </summary>
    public void SetMaxHealth(float max)
    {
        MaxHealth = max; 
    }
}
