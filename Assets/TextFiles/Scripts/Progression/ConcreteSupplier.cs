using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteSupplier : MonoBehaviour, ItemSupplier
{
    [SerializeField] ItemType ItemType;
    [SerializeField] List<Item> items; 

    public List<Item> GetItems()
    {
        return items; 
    }

    public void AddItem(Item item)
    {
        items.Add(item);
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
