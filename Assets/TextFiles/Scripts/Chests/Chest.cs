using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] GameObject DropPrefab;
    [SerializeField] int MaxDrops = 3; 

    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        int drops = Random.Range(1, MaxDrops);
        for (int i = 0; i < drops; i++)
        {
            Instantiate(DropPrefab, (Vector2)transform.position + 1.5f * Random.insideUnitCircle, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
