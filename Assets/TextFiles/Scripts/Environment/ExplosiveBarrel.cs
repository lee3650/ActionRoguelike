using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, LateInitializable
{
    [SerializeField] float fuse = 1f;
    [SerializeField] HealthManager HealthManager;
    [SerializeField] float flashLength = 0.1f;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject collider;

    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        StartCoroutine(TriggerFuse());
    }

    private void ToggleOpacity()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - sr.color.a);
    }

    IEnumerator TriggerFuse()
    {
        float timer = 0f;

        int numFlashes = (int)(fuse / flashLength); 

        while (numFlashes > 0)
        {
            yield return null;
            timer += Time.deltaTime; 
            if (timer > flashLength)
            {
                ToggleOpacity();
                timer = 0f;
                numFlashes--; 
            }
        }

        collider.SetActive(true);

        yield return null;
        yield return null;
        yield return null;

        Destroy(gameObject);
    }
}
