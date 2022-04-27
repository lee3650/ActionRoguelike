using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class XPSlider : MonoBehaviour, LateInitializable
{
    [SerializeField] XPManager XPManager;
    [SerializeField] Slider slider; 

    public void LateInit()
    {
        XPManager.XPChanged += XPChanged;
    }

    private void XPChanged()
    {
        slider.value = XPManager.GetXPPercentage();
    }
}
