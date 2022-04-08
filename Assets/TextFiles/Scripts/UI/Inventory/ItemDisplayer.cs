using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI; 

public class ItemDisplayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] Sprite defaultSprite;
    private Item MyItem;

    private Action<Item> ItemSelected = null;

    public void SetItemSelected(Action<Item> itemSelected)
    {
        ItemSelected = itemSelected; 
    }

    public void Initialize(Item item, Action<Item> itemSelected)
    {
        ItemSelected = itemSelected;
        MyItem = item;
        image.sprite = item.GetSprite();
    }

    public bool HasItem()
    {
        return MyItem != null; 
    }

    public void ResetDisplay()
    {
        image.sprite = defaultSprite;
        ItemSelected = null;
        MyItem = null; 
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (ItemSelected != null)
        {
            ItemSelected(MyItem);
        }
    }
}
