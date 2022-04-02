using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventories : MonoBehaviour
{
    [SerializeField] List<ItemCollection> ItemCollections;
    [SerializeField] SelectedItemDisplay SelectedItemDisplay;

    public void ShowItems(ItemSupplier[] itemSuppliers)
    {
        foreach (ItemSupplier i in itemSuppliers)
        {
            ItemCollection col = GetCollectionOfType(i.ItemType);
            col.ShowItems(i, ItemSelected);
        }
    }

    private ItemCollection GetCollectionOfType(ItemType type)
    {
        foreach (ItemCollection i in ItemCollections)
        {
            if (i.MyItemType == type)
            {
                return i;
            }
        }
        throw new System.Exception("There was no collection of the type " + type);
    }

    private void ItemSelected(Item i, ItemSupplier supplier)
    {
        SelectedItemDisplay.ShowSelectedItem(i, supplier);
    }
}
