using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class FadeOverTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float FadeScale; 

    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Sin(FadeScale * Time.realtimeSinceStartup));
    }
}
