using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleItemActionSupplier : MonoBehaviour, ItemSupplier
{
    public List<ItemType> ItemTypes
    {
        get
        {
            return new List<ItemType>();
        }
    }

    public List<Item> GetItems()
    {
        return new List<Item>();
    }

    public void PerformActionOnItem(Item i, ItemAction a)
    {

    }
}
