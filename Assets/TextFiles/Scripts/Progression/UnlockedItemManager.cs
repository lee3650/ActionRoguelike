using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedItemManager : MonoBehaviour, Initializable
{
    [SerializeField] List<Item> DefaultItems;
    private List<Item> UnlockedItems = new List<Item>(); 

    public void Init()
    {
        for (int i = 0; i < DefaultItems.Count; i++)
        {
            UnlockedItems.Add(DefaultItems[i]); 
        }
    }

    public List<Item> GetUnlockedItems(ItemType type)
    {
        List<Item> result = new List<Item>();
        foreach (Item i in UnlockedItems)
        {
            if (i.ItemType == type)
            {
                result.Add(i); 
            }
        }

        return result;
    }
}
