using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ItemCollection : MonoBehaviour
{
    [SerializeField] ItemDisplayer ItemDisplayerPrefab;
    [SerializeField] Transform ItemGroup; 
    public ItemType MyItemType;
    private ItemSupplier ItemSupplier;

    private System.Action<Item, ItemSupplier> ItemSelected = delegate { };

    private List<ItemDisplayer> shownItems = new List<ItemDisplayer>();

    private void DestroyOldDisplay()
    {
        foreach (ItemDisplayer i in shownItems)
        {
            Destroy(i.gameObject);
        }
        shownItems = new List<ItemDisplayer>();
    }

    public void OnItemSelected(Item i)
    {
        ItemSelected(i, ItemSupplier);
    }

    public void ShowItems(ItemSupplier items, Action<Item, ItemSupplier> itemSelected)
    {
        DestroyOldDisplay();

        ItemSelected = itemSelected;

        ItemSupplier = items; 

        foreach (Item i in items.GetItems())
        {
            ItemDisplayer display = Instantiate(ItemDisplayerPrefab, ItemGroup);
            display.Initialize(i, OnItemSelected);
            shownItems.Add(display); 
        }
    }
}
