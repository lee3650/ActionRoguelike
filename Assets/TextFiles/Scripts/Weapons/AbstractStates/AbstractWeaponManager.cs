using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeaponManager : MonoBehaviour
{
    [SerializeField] GameObject Wielder;

    protected Weapon CurrentWeapon;

    public abstract void StartAction(string s);

    public float GetRange()
    {
        return CurrentWeapon.GetAttackRange();
    }

    public AttackStage GetCurrentStage()
    {
        return CurrentWeapon.GetCurrentStage();
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        CurrentWeapon?.Deselect();
        newWeapon.SetWielder(Wielder?.GetComponent<Targetable>());
        newWeapon.Select();
        CurrentWeapon = newWeapon;
    }

    public bool DoesCurrentWeaponAllowAction(string action)
    {
        if (CurrentWeapon != null)
        {
            print("current weapon can handle: " + action + " " + CurrentWeapon.ActionAllowed(action));
            return CurrentWeapon.ActionAllowed(action);
        }
        return false;
    }

    public bool ActionFinished()
    {
        return CurrentWeapon.ActionFinished();
    }
}
