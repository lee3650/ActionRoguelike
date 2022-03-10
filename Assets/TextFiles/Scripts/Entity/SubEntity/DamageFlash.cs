using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour, SubEntity, Initializable
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float FlashLength = 0.1f;
    [SerializeField] Color flashColor;

    private Color initialColor;

    public void Init()
    {
        //TODO - the material thing later
        initialColor = sr.color; 
    }
    
    public void HandleEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case SignalType.Physical:
                StopAllCoroutines();
                StartCoroutine(Flash());
                break;
        }
    }

    private IEnumerator Flash()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(FlashLength);
        sr.color = initialColor; 
    }
}
