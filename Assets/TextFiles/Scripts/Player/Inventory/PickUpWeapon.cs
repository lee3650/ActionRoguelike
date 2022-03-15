using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour, LateInitializable
{
    [SerializeField] AbstractWeaponManager WeaponManager;
    [SerializeField] Transform WeaponParent;
    [SerializeField] Weapon DefaultWeapon;

    [SerializeField] InjectionSet InjectionSet;

    [SerializeField] private List<Weapon> Inventory = new List<Weapon>();

    public event System.Action<Weapon> PickedUpWeapon = delegate { };

    int selection = 0;

    public void LateInit()
    {
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
        PickedUpWeapon(w);

        Inventory.Add(w);

        InjectionSet.InjectDependencies(w.GetTransform());

        w.Deselect();
        w.transform.parent = WeaponParent;
        w.transform.localEulerAngles = Vector3.zero;
        w.transform.localPosition = w.GetRelativePosition();
    }

    /// <summary>
    /// Test method. Not for production use. 
    /// </summary>
    public void SetInjectors(Component[] injectors)
    {
        if (InjectionSet == null)
        {
            InjectionSet = gameObject.AddComponent<InjectionSet>();
            InjectionSet.SetInjectors(injectors);
            InjectionSet.Init();
        }
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
