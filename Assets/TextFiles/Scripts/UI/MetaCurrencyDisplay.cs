using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetaCurrencyDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string prepend = "Woolongs: ";
    // Update is called once per frame
    void Update()
    {
        text.text = prepend + MetaCurrencyManager.Balance;   
    }
}
