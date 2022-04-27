using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ManaSlider : MonoBehaviour, Initializable
{
    [SerializeField] PlayerGetter PlayerGetter; 
    [SerializeField] Slider slider;
    [SerializeField] Color unchargedColor;
    [SerializeField] Color chargedColor;
    [SerializeField] Image sliderImage; 
    ManaManager ManaManager;

    public void Init()
    {
        PlayerGetter.PlayerReady += PlayerGetter_PlayerReady;
    }

    private void PlayerGetter_PlayerReady(Transform obj)
    {
        ManaManager = obj.GetComponent<ManaManager>();
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
