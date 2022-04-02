using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI; 

public class ItemDisplayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image; 
    private Item MyItem;

    private Action<Item> ItemSelected;

    public void Initialize(Item item, Action<Item> itemSelected)
    {
        ItemSelected = itemSelected;
        MyItem = item;
        image.sprite = item.GetSprite();
    }

    public void OnPointerClick(PointerEventData data)
    {
        ItemSelected(MyItem);
    }
}
