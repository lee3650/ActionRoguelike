using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemSupplier : MonoBehaviour, ItemSupplier
{
    [SerializeField] PickUpWeapon Inventory; 

    public ItemType ItemType
    {
        get
        {
            return ItemType.Weapon; 
        }
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();
        foreach (Weapon w in Inventory.GetInventory())
        {
            if (w.TryGetComponent<Item>(out Item i))
            {
                items.Add(i);
            } else
            {
                throw new System.Exception(string.Format("Weapon {0} was not associated with an item!", w.name));
            }
        }
        return items; 
    }

    public void PerformActionOnItem(Item i, ItemAction a)
    {
        //actually I don't really know of any actions we could do here. 
    }
}
