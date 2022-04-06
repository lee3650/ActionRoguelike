using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public abstract class AbstractGearManager : MonoBehaviour, ItemSupplier
{
    public event Action<ItemType, GameObject> GearEquipped = delegate { };
    protected Dictionary<ItemType, GameObject> EquippedItems = new Dictionary<ItemType, GameObject>();

    public void Init()
    {
        foreach (ItemType t in Enum.GetValues(typeof(ItemType)))
        {
            EquippedItems[t] = null;
        }
    }

    public GameObject GetEquippedItem(ItemType i)
    {
        return EquippedItems[i];
    }

    public bool HasEquippedItem(ItemType t)
    {
        return EquippedItems[t] != null;
    }

    public abstract List<ItemType> ItemTypes
    {
        get;
    }

    public abstract List<Item> GetItems();

    protected abstract void PartialPerformAction(Item item, ItemAction action);

    private void DequipItem(Gear item)
    {
        EquippedItems[item.ItemType] = null;
        item.Equipped = false;
        GearEquipped(item.ItemType, null);
        PartialPerformAction(item.GetComponent<Item>(), ItemAction.Dequip);
    }

    public void PerformActionOnItem(Item item, ItemAction action)
    {
        Gear g = item.GetComponent<Gear>();

        switch (action)
        {
            case ItemAction.Equip:
                Debug.Assert(g != null);

                if (EquippedItems[g.ItemType] != null)
                {
                    DequipItem(g);
                }

                EquippedItems[g.ItemType] = item.gameObject;
                g.Equipped = true;
                GearEquipped(g.ItemType, item.gameObject);

                //we have to call this individually in all the methods if they call each other
                PartialPerformAction(item, action);
                break;
            case ItemAction.Dequip:
                DequipItem(g); 
                break;
        }
    }
}
