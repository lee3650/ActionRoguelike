using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteSupplier : MonoBehaviour, ItemSupplier
{
    [SerializeField] ItemType ItemType;
    [SerializeField] List<Item> items;
    [SerializeField] UnlockedItemManager UnlockedItemManager;

    public List<Item> GetItems()
    {
        return UnlockedItemManager.GetUnlockedItems(ItemType); 
    }

    public List<ItemType> ItemTypes
    {
        get
        {
            return new List<ItemType>() { ItemType }; 
        }
    }

    public void PerformActionOnItem(Item item, ItemAction action)
    {

    }
}
