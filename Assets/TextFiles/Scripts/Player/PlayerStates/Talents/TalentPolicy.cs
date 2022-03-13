using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentPolicy : MonoBehaviour
{
    [SerializeField] private bool upgradable = false; 

    public abstract void ApplyPolicy();
    public abstract void ApplyUpgrade(int index);
    public abstract TalentInfo GetNextUpgradeInfo();

    public bool Upgradable
    {
        get
        {
            return upgradable;
        }
        protected set
        {
            upgradable = value; 
        }
    }
}
