using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] PickUpWeapon Inventory;
    [SerializeField] float reach; 

    private List<Weapon> GetNearbyWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, reach);
        foreach (Collider2D col in hits)
        {
            if (col.TryGetComponent<Weapon>(out Weapon w))
            {
                print("found " + w);
                if (w.CanPickUp() && !Inventory.Contains(w))
                {
                    print("adding " + w);
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
                Inventory.AddToInventory(w);
            }
        }
    }
}
