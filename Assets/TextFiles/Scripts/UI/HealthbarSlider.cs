using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthbarSlider : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] Slider Slider;

    public void LateInit()
    {
        HealthManager.HealthChanged += HealthChanged;
    }

    private void HealthChanged()
    {
        Slider.value = HealthManager.GetHealthPercentage();
    }
}
