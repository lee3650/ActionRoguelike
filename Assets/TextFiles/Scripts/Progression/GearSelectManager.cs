using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSelectManager : MonoBehaviour, LateInitializable
{
    [SerializeField] GameObject SelectionParent; 
    [SerializeField] ProgressionGearManager ProgressionGearManager;
    [SerializeField] EquippedGearDisplayer EquippedGearDisplayer;
    [SerializeField] SelectedItemDisplay SelectedItemDisplay;
    [SerializeField] ItemCollection ItemCollection;
    [SerializeField] ConcreteSupplier[] CorrespondingItemSuppliers;
    [SerializeField] HandleItemActionSupplier ItemActionSupplier;

    public void LateInit()
    {
        EquippedGearDisplayer.DisplayGear(ProgressionGearManager, SlotSelected);
        SelectionParent.SetActive(false);
    }

    private void SlotSelected(Item i, ItemSupplier s)
    {
        if (i != null)
        {
            ItemCollection.ShowItems(GetItemSupplier(i.ItemType), ItemSelected);
            SelectionParent.SetActive(true);
            ItemSelected(i, s); 
        }
    }

    public void SlotSelected(ItemType type)
    {
        print("showing items " + type); 
        ItemCollection.ShowItems(GetItemSupplier(type), ItemSelected);
        SelectionParent.SetActive(true);
    }

    private ItemSupplier GetItemSupplier(ItemType type)
    {
        for (int i = 0; i < CorrespondingItemSuppliers.Length; i++)
        {
            if (CorrespondingItemSuppliers[i].ItemTypes[0] == type)
            {
                return CorrespondingItemSuppliers[i];
            }
        }

        throw new System.Exception("No supplier for type " + type); 
    }

    private void ItemSelected(Item item, ItemSupplier supplier)
    {
        SelectedItemDisplay.ShowSelectedItem(item, ProgressionGearManager); 
    }
}
