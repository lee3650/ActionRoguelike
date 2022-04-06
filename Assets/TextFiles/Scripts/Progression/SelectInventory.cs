using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventory : MonoBehaviour
{
    private static Dictionary<ItemType, GameObject> TypeToItem = new Dictionary<ItemType, GameObject>();

    public static void SetStartingItem(ItemType type, GameObject item)
    {
        TypeToItem[type] = item;
    }

    public static GameObject GetStartingItem(ItemType type)
    {
        if (TypeToItem.ContainsKey(type))
        {
            return TypeToItem[type];
        }
        return null; 
    }
}
