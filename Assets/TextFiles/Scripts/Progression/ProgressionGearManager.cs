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
        throw new System.NotImplementedException();
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
