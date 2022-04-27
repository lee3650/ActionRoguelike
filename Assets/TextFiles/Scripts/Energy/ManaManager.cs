using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ManaManager : MonoBehaviour, Initializable, StatListener, LateInitializable
{
    [SerializeField] StatsList stats;
    [SerializeField] float curMana = 0f;
    [SerializeField] float ChargeAmt = 10;

    float maxMana;

    public event Action ManaChanged = delegate { };

    const string maxManaStat = "maxMana"; 

    public void Init()
    {
        curMana = 0f; 
    }

    public void LateInit()
    {
        stats.RegisterListener(maxManaStat, this);
        maxMana = stats.GetStat(maxManaStat); 
    }

    public void StatChanged(string s, float v)
    {
        maxMana = v; 
    }

    public void LerpCharge(float length, float delta)
    {
        float diff = (ChargeAmt * delta) / length;
        curMana -= diff;
        ManaChanged();
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
