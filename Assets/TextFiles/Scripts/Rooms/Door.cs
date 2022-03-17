using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Collider2D col;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float length = 0.25f; 

    public void Open()
    {
        col.enabled = false;
        StopAllCoroutines();
        StartCoroutine(Fade(1, 0, length));
    }

    public void Close()
    {
        col.enabled = true;
        StopAllCoroutines();
        StartCoroutine(Fade(0, 1, length));
    }

    private IEnumerator Fade(float start, float end, float len)
    {
        float timer = 0f; 
        while (timer < len)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(start, end, timer / len));
            timer += Time.deltaTime;
            yield return null; 
        }
    } 
}
