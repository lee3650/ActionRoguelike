using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour, Initializable
{
    [SerializeField] float SquashAmount;
    [SerializeField] float SquashTime;
    [SerializeField] float RecoveryTime;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb; 

    Vector3 originalScale;

    public void Init()
    {
        originalScale = transform.localScale;
    }

    public void StartSquash()
    {
        StopAllCoroutines();
        StartCoroutine(ApplySquash());
    }

    IEnumerator ApplySquash()
    {
        float timer = 0f;

        float bottomEdge = GetBottomEdge();

        while (timer <= SquashTime)
        {
            yield return null;
            timer += Time.deltaTime;

            float curSquash = Mathf.Lerp(1f, SquashAmount, timer / SquashTime);

            bottomEdge = GetBottomEdge();

            SetScale(curSquash);

            AdjustPosition(bottomEdge);
        }

        timer = 0f;

        while (timer <= RecoveryTime)
        {
            yield return null;
            timer += Time.deltaTime;

            float curSquash = Mathf.Lerp(SquashAmount, 1f, timer / RecoveryTime);

            bottomEdge = GetBottomEdge();

            SetScale(curSquash);

            AdjustPosition(bottomEdge);
        }

        transform.localScale = originalScale;
    }

    float GetBottomEdge()
    {
        return transform.position.y - sr.bounds.extents.y;
    }

    private void SetScale(float curSquash)
    {
        transform.localScale = new Vector3(1f / curSquash * originalScale.x, curSquash * originalScale.y, 1f);
    }

    private void AdjustPosition(float bottomEdge)
    {
        transform.position = new Vector3(transform.position.x, bottomEdge + sr.bounds.extents.y, transform.position.z);
    }

}
