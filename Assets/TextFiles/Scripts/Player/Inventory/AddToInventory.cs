using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    [SerializeField] PickUpWeapon Inventory;
    [SerializeField] float reach; 

    public Weapon QueuedWeapon
    {
        get;
        private set; 
    }

    public void FindQueuedWeapon()
    {
        Weapon w = GetNearbyStuckWeapon();
        QueuedWeapon = w; 
    }

    public float GetPickupDelay()
    {
        return QueuedWeapon.GetComponent<PickupLength>().LengthOfPickup; 
    }

    public bool HasQueuedWeapon()
    {
        return QueuedWeapon != null;
    }

    public void PickupQueued()
    {
        PickupWeaponNow(QueuedWeapon);
    }

    private void PickupWeaponNow(Weapon w)
    {
        Inventory.AddToInventory(w);
    }

    private Weapon PickupWeaponFromCollision(Collider2D col)
    {
        if (col.TryGetComponent<Weapon>(out Weapon w))
        {
            if (CanPickUp(w))
            {
                return w;
            }
        }

        return null; 
    }

    public Weapon GetNearbyStuckWeapon()
    {
        List<Weapon> nearWeapons = GetNearbyWeapons();
        
        foreach (Weapon w in nearWeapons)
        {
            if (!CanPickUpWeaponNow(w))
            {
                return w; 
            }
        }

        return null; 
    }

    private List<Weapon> GetNearbyWeapons()
    {
        List<Weapon> nearWeapons = new List<Weapon>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, reach);

        foreach (Collider2D col in hits)
        {
            Weapon w = PickupWeaponFromCollision(col);
            if (w != null)
            {
                nearWeapons.Add(w);
            }
        }
        return nearWeapons; 
    }

    private void FixedUpdate()
    {
        List<Weapon> nearWeapons = GetNearbyWeapons();
        foreach (Weapon w in nearWeapons)
        {
            print("found weapon " + w);
            if (CanPickUpWeaponNow(w))
            {
                PickupWeaponNow(w);
            }
        }
    }

    private bool CanPickUpWeaponNow(Weapon w)
    {
        if (w.TryGetComponent<PickupLength>(out PickupLength len))
        {
            return len.LengthOfPickup == 0;
        }
        return true; 
    }

    private bool CanPickUp(Weapon w)
    {
        return w.CanPickUp() && !Inventory.Contains(w);
    }
}
