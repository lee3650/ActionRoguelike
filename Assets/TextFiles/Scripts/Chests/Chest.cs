using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] Weapon[] PossibleWeapons; 

    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Weapon w = PossibleWeapons[Random.Range(0, PossibleWeapons.Length)];
        Instantiate(w, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
