using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShower : MonoBehaviour, Initializable, Dependency<DisplayInventories>
{
    DisplayInventories display; 
    ItemSupplier[] mySuppliers;

    public void InjectDependency(DisplayInventories di)
    {
        display = di; 
    }

    public void Init()
    {
        mySuppliers = GetComponents<ItemSupplier>();
    }

    public void ShowInventory()
    {
        display.ShowItems(mySuppliers);
    }

    public void HideInventory()
    {
        display.Hide();
    }
}
