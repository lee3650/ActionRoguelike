using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour, Initializable, LateInitializable
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float FlashLength = 0.1f;
    [SerializeField] Color flashColor;
    [SerializeField] HealthManager HealthManager;

    private Color initialColor;

    public void Init()
    {
        //TODO - the material thing later
        initialColor = sr.color; 
    }

    public void LateInit()
    {
        HealthManager.DamageTaken += DamageTaken;
    }

    private void DamageTaken()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(FlashLength);
        if (HealthManager.IsAlive())
        {
            sr.color = initialColor;
        }
    }
}
