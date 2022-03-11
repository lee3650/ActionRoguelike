using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour, Initializable
{
    [SerializeField] float lifeSpan;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float fadeLength; 

    public void Init()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(lifeSpan);
        float timer = 0;
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - (timer / fadeLength)); 
            if (timer >= fadeLength)
            {
                Destroy(gameObject);
            }
        }
    }
}
