using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventories : MonoBehaviour
{
    [SerializeField] List<ItemCollection> ItemCollections;
    [SerializeField] SelectedItemDisplay SelectedItemDisplay;
    [SerializeField] EquippedGearDisplayer EquippedGearDisplayer;
    [SerializeField] GameObject inventoryParent; 

    public void ShowItems(ItemSupplier[] itemSuppliers, GearManager gear)
    {
        inventoryParent.SetActive(true);
        foreach (ItemSupplier i in itemSuppliers)
        {
            ItemCollection col = GetCollectionOfType(i.ItemTypes);
            col.ShowItems(i, ItemSelected);
        }
        EquippedGearDisplayer.DisplayGear(gear, ItemSelected);
    }

    public void Hide()
    {
        inventoryParent.SetActive(false);
    }

    private ItemCollection GetCollectionOfType(List<ItemType> types)
    {
        foreach (ItemCollection i in ItemCollections)
        {
            bool held = true; 
            foreach (ItemType type in types)
            {
                if (!i.HoldsItemType(type))
                {
                    held = false;
                }
            }
            if (held)
            {
                return i; 
            }
        }
        throw new System.Exception("There was no collection of the type " + types.ToString());
    }

    private void ItemSelected(Item i, ItemSupplier supplier)
    {
        SelectedItemDisplay.ShowSelectedItem(i, supplier);
    }
}
