using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaCurrencyManager : MonoBehaviour, Initializable
{
    public static int Balance = 0;
    public static event System.Action<int> BalanceChanged = delegate { };

    [SerializeField] int startingBalance = 12; 

    public static void SpendBalance(int amt)
    {
        Balance -= amt;
        BalanceChanged(Balance); 
    }

    public void Init()
    {
        Balance = startingBalance; 
    }
}
