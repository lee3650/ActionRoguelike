using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventory
{
    private static Dictionary<ItemType, GameObject> TypeToItem = new Dictionary<ItemType, GameObject>();

    public static void SetStartingItem(ItemType type, GameObject item)
    {
        MonoBehaviour.print("set item type " + type + " to item " + item); 
        TypeToItem[type] = item;
    }

    public static GameObject GetStartingItem(ItemType type)
    {
        if (TypeToItem.ContainsKey(type))
        {
            MonoBehaviour.print("contained key " + type);
            return TypeToItem[type];
        }
        MonoBehaviour.print("didn't contain key " + type);
        return null; 
    }

    public static void Reset()
    {
        TypeToItem = new Dictionary<ItemType, GameObject>(); 
    }
}
