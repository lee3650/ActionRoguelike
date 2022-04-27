using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShower : MonoBehaviour, Initializable, Dependency<DisplayInventories>
{
    [SerializeField] GearManager gearManager;
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
        display.ShowItems(mySuppliers, gearManager);
    }

    public void HideInventory()
    {
        display.Hide();
    }
}
