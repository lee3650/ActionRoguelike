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

    private float currentHealth; 

    public void Init()
    {
        currentHealth = MaxHealth; 
    }

    public void TakeDamage(float amt)
    {
        currentHealth -= amt;
        DamageTaken();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath();
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

}
