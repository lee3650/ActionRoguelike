using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupText : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image Background;

    private bool visible = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!visible)
        {
            text.color = UtilityFunctions.LerpAlpha(text.color, 0, 1, 1);
            Background.color = UtilityFunctions.LerpAlpha(Background.color, 0, 1, 1);
            visible = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Show(string message, float fadeLength)
    {
        text.color = UtilityFunctions.LerpAlpha(text.color, 1, 0, 1);
        Background.color = UtilityFunctions.LerpAlpha(Background.color, 1, 0, 1);
        text.text = message;
        gameObject.SetActive(true);
        visible = false;
        StartCoroutine(FadeIn(fadeLength));
    }

    IEnumerator FadeIn(float len)
    {
        float timer = 0f; 
        while (timer < len)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            timer += Time.unscaledDeltaTime;
            text.color = UtilityFunctions.LerpAlpha(text.color, 0, 1, timer / len);
            Background.color = UtilityFunctions.LerpAlpha(Background.color, 0, 1, timer / len);
        }

        visible = true;
    }
}
