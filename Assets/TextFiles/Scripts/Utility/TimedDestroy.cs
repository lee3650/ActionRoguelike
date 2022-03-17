using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour, LateInitializable
{
    [SerializeField] private float Lifespan; 
    public void LateInit()
    {
        StartCoroutine(DestroyAfterLifespan());
    }

    IEnumerator DestroyAfterLifespan()
    {
        yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
    }
}
