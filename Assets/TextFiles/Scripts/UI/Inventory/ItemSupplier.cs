using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemSupplier
{
    public List<Item> GetItems();
    public ItemType ItemType
    {
        get;
    }

    public void PerformActionOnItem(Item i, ItemAction a);
}
