using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class AIHealthBar : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    private List<GameObject> HealthChunks = new List<GameObject>();

    public void LateInit()
    {
        HealthManager.HealthChanged += HealthChanged;
        HealthManager.OnDeath += OnDeath;
        int chunkNum = Mathf.RoundToInt(HealthManager.GetCurHealth());

    }

    private void Update()
    {
        
    }

    private void OnDeath()
    {

    }

    private void HealthChanged()
    {
    }
}
