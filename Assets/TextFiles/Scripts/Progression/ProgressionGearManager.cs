using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionGearManager : AbstractGearManager
{
    public override List<Item> GetItems()
    {
        return new List<Item>();
    }

    protected override void PartialPerformAction(Item item, ItemAction action)
    {
        switch (action)
        {
            case ItemAction.Equip:
                SelectInventory.SetStartingItem(item.ItemType, item.gameObject);
                print("setting starting item for " + item); 
                break;
            case ItemAction.Dequip:
                SelectInventory.SetStartingItem(item.ItemType, null);
                print("unsetting starting item for " + item);
                break;
        }
    }

    public override List<ItemType> ItemTypes
    {
        get { 
            return new List<ItemType>() 
            {
                ItemType.Amulet, 
                ItemType.Armor, 
                ItemType.Ring, 
                ItemType.Weapon 
            }; 
        }
    }
}
