using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ManaManager : MonoBehaviour, Initializable
{
    [SerializeField] float maxMana = 30;
    [SerializeField] float curMana = 0f;
    [SerializeField] float ChargeAmt = 10;

    public event Action ManaChanged = delegate { };

    public void Init()
    {
        curMana = 0f; 
    }

    public bool ChargesRemaining(int number)
    {
        return curMana >= number * ChargeAmt;
    }

    public float GetManaPercent()
    {
        return curMana / maxMana; 
    }

    public void UseCharge()
    {
        curMana -= ChargeAmt;
        ManaChanged();
    }

    public void AddMana(float amt)
    {
        curMana += amt; 
        if (curMana > maxMana)
        {
            curMana = maxMana; 
        }
        ManaChanged();
    }
}
