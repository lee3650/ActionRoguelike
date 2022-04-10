using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class BalanceText : MonoBehaviour, LateInitializable
{
    [SerializeField] TextMeshProUGUI Text; 

    public void LateInit()
    {
        MetaCurrencyManager.BalanceChanged += BalanceChanged;
        BalanceChanged(MetaCurrencyManager.Balance); 
    }

    private void BalanceChanged(int obj)
    {
        Text.text = string.Format("You have {0} woolongs", obj);
    }
}
