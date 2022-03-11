using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripFromHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer DripPrefab;
    [SerializeField] int maxDrip = 2;
    [SerializeField] float minDripFreq = 0.1f; 
    [SerializeField] float maxDripFreq = 1f; 
    [SerializeField] HealthManager HealthManager;
    [SerializeField] float radius; 

    [SerializeField] private float curFreq = 0f;
    [SerializeField] private int curDrip = 0;

    private float timer = 0f; 

    private void Update()
    {
        curFreq = Mathf.Lerp(maxDripFreq, minDripFreq, 1 - HealthManager.GetHealthPercentage());
        curDrip = Mathf.RoundToInt(Mathf.Lerp(0, maxDrip, 1 - HealthManager.GetHealthPercentage()));

        print("health percentage: " + HealthManager.GetHealthPercentage());

        timer += Time.deltaTime;

        if (timer > curFreq)
        {
            timer = 0f; 

            for (int i = 0; i < curDrip; i++)
            {
                //maybe try random z rotation
                Instantiate(DripPrefab, (Vector2)transform.position + (Random.insideUnitCircle * radius), Quaternion.identity);
            }
        }
    }
}
