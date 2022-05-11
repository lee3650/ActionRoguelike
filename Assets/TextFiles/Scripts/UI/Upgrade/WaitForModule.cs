using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForModule : MonoBehaviour
{
    [SerializeField] XPManager XPManager;
    [SerializeField] GameObject CloseButton;

    void Update()
    {
        if (XPManager.HasCurrentPolicy() && XPManager.GetXPPercentage() < 1)
        {
            CloseButton.SetActive(true);
        }
        else
        {
            CloseButton.SetActive(false);
        }
    }
}
