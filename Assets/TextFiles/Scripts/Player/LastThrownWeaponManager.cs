using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LastThrownWeaponManager : MonoBehaviour, LateInitializable
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PickUpWeapon PickUpWeapon;

    public event System.Action PickedUpLastWeapon = delegate { }; 

    private List<Weapon> thrownWeapons = new List<Weapon>();

    public void LateInit()
    {
        WeaponManager.StartedAction += StartedAction;
        PickUpWeapon.PickedUpWeapon += PickedUpWeapon;
    }

    public bool HasThrownWeapon()
    {
        return thrownWeapons.Count > 0; 
    }

    public Weapon GetLastThrownWeapon()
    {
        return thrownWeapons[thrownWeapons.Count - 1];
    }

    private void PickedUpWeapon(Weapon obj)
    {
        thrownWeapons.Remove(obj);
        PickedUpLastWeapon();
        print("removing " + obj + ", new count " + thrownWeapons.Count);
    }

    private void StartedAction(string obj)
    {
        if (obj.Equals(ActionStrings.ThrowAction) || obj.Equals(ActionStrings.SuperThrow))
        {
            thrownWeapons.Add(WeaponManager.GetCurrentWeapon());
        }
    }
}
