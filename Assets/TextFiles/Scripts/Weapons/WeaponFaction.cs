using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFaction : MonoBehaviour
{
    [SerializeField] private Factions MyFaction;

    public event System.Action<Factions> FactionChanged; 

    public void SetFaction(Factions f)
    {
        MyFaction = f;
        FactionChanged(f);
    }

    public Factions GetFaction()
    {
        return MyFaction; 
    }
}
