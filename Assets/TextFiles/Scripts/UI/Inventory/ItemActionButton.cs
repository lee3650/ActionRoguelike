using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; 

public class ItemActionButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    private SelectedItemDisplay SelectedItemDisplay;
    private ItemAction itemAction; 

    public void Initialize(ItemAction actionType, SelectedItemDisplay display)
    {
        itemAction = actionType;
        SelectedItemDisplay = display;
        text.text = actionType.ToString();
    }

    public void OnPointerClick(PointerEventData data)
    {
        SelectedItemDisplay.ActionClicked(itemAction);
    }
}
