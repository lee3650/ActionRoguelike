using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItemAction : MonoBehaviour
{
    [SerializeField] SelectedItemDisplay SelectedItemDisplay;
    [SerializeField] ItemAction ItemAction; 
    public void CallAction()
    {
        SelectedItemDisplay.ActionClicked(ItemAction);
    }
}
