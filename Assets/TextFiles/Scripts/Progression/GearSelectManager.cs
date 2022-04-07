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

    public void LateInit()
    {
        EquippedGearDisplayer.DisplayGear(ProgressionGearManager, SlotSelected);
        SelectionParent.SetActive(false);
    }

    private void SlotSelected(Item i, ItemSupplier s)
    {
        SelectionParent.SetActive(true);

        //s = just the gear manager, so kinda irrelevant. 
        if (s.ItemTypes.Count > 1)
        {
            throw new System.Exception("Item suppliers on the selection manager should have only 1 item type!"); 
        }

        ItemCollection.ShowItems(GetItemSupplier(s.ItemTypes[0]), ItemSelected);

        if (i != null)
        {
            ItemSelected(i, s); 
        }
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
        SelectedItemDisplay.ShowSelectedItem(item, supplier); 
    }
}
