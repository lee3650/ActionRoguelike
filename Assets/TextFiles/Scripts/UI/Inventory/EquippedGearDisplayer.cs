using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedGearDisplayer : MonoBehaviour
{
    [SerializeField] ItemType[] GearTypes;
    [SerializeField] ItemDisplayer[] CorrespondingDisplayers;

    private AbstractGearManager currentGm = null;

    private System.Action<Item, ItemSupplier> selectedItemAction = delegate { };

    public void DisplayGear(AbstractGearManager gm, System.Action<Item, ItemSupplier> action)
    {
        selectedItemAction = action; 
        if (currentGm != null)
        {
            currentGm.GearEquipped -= ShowGearInSlot;
        }
        currentGm = gm;
        currentGm.GearEquipped += ShowGearInSlot;

        foreach (ItemType t in System.Enum.GetValues(typeof(ItemType)))
        {
            ShowGearInSlot(t, gm.GetEquippedItem(t));
        }
    }

    public void ItemSelected(Item item)
    {
        selectedItemAction(item, currentGm);
    }

    private ItemDisplayer TryGetItemDisplayer(ItemType t)
    {
        for (int i = 0; i < GearTypes.Length; i++)
        {
            if (GearTypes[i] == t)
            {
                return CorrespondingDisplayers[i];
            }
        }
        return null;
    }

    private void ShowGearInSlot(ItemType arg1, GameObject arg2)
    {
        ItemDisplayer i = TryGetItemDisplayer(arg1);

        if (arg2 != null && i == null)
        {
            throw new System.Exception("Could not find an item dislayer for item type " + arg1);
        }
        else if (arg2 == null && i == null)
        {
            return; 
        }

        if (arg2 != null)
        {
            i.Initialize(arg2.GetComponent<Item>(), ItemSelected);
        }
        else
        {
            i.ResetDisplay();
        }
    }
}