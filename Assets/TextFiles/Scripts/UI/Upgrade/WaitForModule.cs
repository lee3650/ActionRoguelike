using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForModule : MonoBehaviour
{
    [SerializeField] XPManager XPManager;
    [SerializeField] GameObject CloseButton;

    void Update()
    {
        if (XPManager.HasPolicyInProgress())
        {
            CloseButton.SetActive(true);
        }
        else
        {
            CloseButton.SetActive(false);
        }
    }
}
