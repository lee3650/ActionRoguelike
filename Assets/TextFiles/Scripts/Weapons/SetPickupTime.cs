using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPickupTime : MonoBehaviour, LateInitializable
{
    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] PickupLength PickupLength;
    [SerializeField] float[] PickupTimes = new float[] { 0, 1 };
    [SerializeField] string[] Actions = new string[] { ActionStrings.AttackAction, ActionStrings.ThrowAction };

    public void LateInit()
    {
        MyWeapon.OnStartAction += OnStartAction;
    }

    private void OnStartAction(string obj)
    {
        for (int i = 0; i < PickupTimes.Length; i++)
        {
            if (obj.Equals(Actions[i]))
            {
                PickupLength.LengthOfPickup = PickupTimes[i];
            }
        }
    }
}
