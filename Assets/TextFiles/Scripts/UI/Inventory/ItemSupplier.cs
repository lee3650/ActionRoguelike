using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemSupplier
{
    public List<Item> GetItems();

    public List<ItemType> ItemTypes
    {
        get;
    }

    public void PerformActionOnItem(Item i, ItemAction a);
}
