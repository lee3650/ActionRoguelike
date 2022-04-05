using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedGearDisplayer : MonoBehaviour
{
    [SerializeField] GearType[] GearTypes;
    [SerializeField] ItemDisplayer[] CorrespondingDisplayers;

    private GearManager currentGm = null;

    private System.Action<Item, ItemSupplier> selectedItemAction = delegate { };

    public void DisplayGear(GearManager gm, System.Action<Item, ItemSupplier> action)
    {
        selectedItemAction = action; 
        if (currentGm != null)
        {
            currentGm.GearEquipped -= ShowGearInSlot;
        }
        currentGm = gm;
        currentGm.GearEquipped += ShowGearInSlot;

        foreach (GearType t in System.Enum.GetValues(typeof(GearType)))
        {
            ShowGearInSlot(t, gm.GetEquippedGear(t));
        }
    }

    public void ItemSelected(Item item)
    {
        selectedItemAction(item, currentGm);
    }

    private ItemDisplayer GetItemDisplayer(GearType g)
    {
        for (int i = 0; i < GearTypes.Length; i++)
        {
            if (GearTypes[i] == g)
            {
                return CorrespondingDisplayers[i];
            }
        }
        throw new System.Exception("Could not find item displayer for gear type " + g);
    }

    private void ShowGearInSlot(GearType arg1, Gear arg2)
    {
        ItemDisplayer i = GetItemDisplayer(arg1);

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
