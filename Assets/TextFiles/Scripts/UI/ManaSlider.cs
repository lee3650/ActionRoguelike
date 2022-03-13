using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ManaSlider : MonoBehaviour, LateInitializable
{
    [SerializeField] ManaManager ManaManager;
    [SerializeField] Slider slider;
    [SerializeField] Color unchargedColor;
    [SerializeField] Color chargedColor;
    [SerializeField] Image sliderImage; 

    public void LateInit()
    {
        ManaManager.ManaChanged += ManaChanged;
        sliderImage.color = unchargedColor; 
    }

    private void ManaChanged()
    {
        slider.value = ManaManager.GetManaPercent();
        if (ManaManager.ChargesRemaining(1))
        {
            sliderImage.color = chargedColor;
        } else
        {
            sliderImage.color = unchargedColor; 
        }
    }
}
