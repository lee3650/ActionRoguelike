using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScrapBudget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ProgressionOptionSupplier ProgressionOptionSupplier;

    void Update()
    {
        text.text = string.Format("Scrap Remaining: {0}", ProgressionOptionSupplier.GetAvailableScrap());
    }
}
