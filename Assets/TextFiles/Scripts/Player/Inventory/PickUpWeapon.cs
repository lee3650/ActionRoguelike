using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour, LateInitializable
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] Transform WeaponParent;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] float reach; 

    [SerializeField] private List<Weapon> Inventory = new List<Weapon>();

    int selection = 0;

    public void LateInit()
    {
        Inventory.Add(WeaponManager.GetCurrentWeapon());
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

    private void AddToInventory(Weapon w)
    {
        Inventory.Add(w);
        w.Deselect();
        w.transform.parent = WeaponParent;
        w.transform.localEulerAngles = Vector3.zero;
        w.transform.localPosition = Vector3.zero; 
    }

    private List<Weapon> GetNearbyWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, reach);
        foreach (Collider2D col in hits)
        {
            if (col.TryGetComponent<Weapon>(out Weapon w))
            {
                if (w.CanPickUp() && !Inventory.Contains(w))
                {
                    weapons.Add(w);
                }
            }
        }

        return weapons; 
    }

    private void Update()
    {
        if (PlayerInput.PickUpItems())
        {
            List<Weapon> weapons = GetNearbyWeapons();
            foreach (Weapon w in weapons)
            {
                AddToInventory(w);
            }
        }
    }
}
