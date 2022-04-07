using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public abstract class AbstractGearManager : MonoBehaviour, ItemSupplier, Initializable
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
        print("key: " + i); 
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

    private void DequipItem(Item item)
    {
        EquippedItems[item.ItemType] = null;
        GearEquipped(item.ItemType, null);
        PartialPerformAction(item, ItemAction.Dequip);
    }

    public void PerformActionOnItem(Item item, ItemAction action)
    {
        switch (action)
        {
            case ItemAction.Equip:
                if (EquippedItems[item.ItemType] != null)
                {
                    DequipItem(item);
                }

                EquippedItems[item.ItemType] = item.gameObject;
                GearEquipped(item.ItemType, item.gameObject);

                //we have to call this INDIVIDUALLY in all the methods since they call each other
                PartialPerformAction(item, action);
                break;
            case ItemAction.Dequip:
                DequipItem(item); 
                break;
        }
    }
}
