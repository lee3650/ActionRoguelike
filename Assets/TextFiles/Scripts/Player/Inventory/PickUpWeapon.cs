using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour, LateInitializable
{
    [SerializeField] AbstractWeaponManager WeaponManager;
    [SerializeField] Transform WeaponParent;
    [SerializeField] Weapon DefaultWeapon; 

    [SerializeField] private List<Weapon> Inventory = new List<Weapon>();

    [SerializeField] Component[] weaponDependencyInjectors;

    private List<IDependencyInjector> DependencyInjectors;

    int selection = 0;

    public void LateInit()
    {
        DependencyInjectors = new List<IDependencyInjector>();
        foreach (Component c in weaponDependencyInjectors)
        {
            IDependencyInjector di = c as IDependencyInjector;
            if (di == null)
            {
                throw new System.Exception("Dependency injector " + c + " was null!");
            }
            DependencyInjectors.Add(di);
        }

        AddToInventory(DefaultWeapon);
        WeaponManager.SelectWeapon(Inventory[0]);
        selection = 0;
    }

    public void ChangeSelection(int dir)
    {
        selection += dir; 

        if (selection >= Inventory.Count)
        {
            selection = 0;
        }
        if (selection < 0)
        {
            selection = Inventory.Count - 1;
        }

        WeaponManager.SelectWeapon(Inventory[selection]);
    }

    public void RemoveSelectedWeapon()
    {
        Inventory.RemoveAt(selection);
        ChangeSelection(-1);
    }

    public bool Contains(Weapon w)
    {
        return Inventory.Contains(w);
    }

    public void AddToInventory(Weapon w)
    {
        Inventory.Add(w);
        w.Deselect();
        w.transform.parent = WeaponParent;
        w.transform.localEulerAngles = Vector3.zero;
        w.transform.localPosition = w.GetRelativePosition(); 

        foreach (IDependencyInjector di in DependencyInjectors)
        {
            di.InjectDependencies(w.GetTransform());
        }
    }

    /// <summary>
    /// Test method. Not for production use. 
    /// </summary>
    public void SetInjectors(Component[] injectors)
    {
        weaponDependencyInjectors = injectors; 
    }

    /// <summary>
    /// Test method. Not for production use. 
    /// </summary>
    public void SetWeaponManager(AbstractWeaponManager awm)
    {
        WeaponManager = awm;
    }

    /// <summary>
    /// Test method. Not for production use. 
    /// </summary>
    public void SetDefaultWeapon(Weapon w)
    {
        DefaultWeapon = w; 
    }
}
