using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaCurrencyManager : MonoBehaviour
{
    public static int Balance = 0;
    public static event System.Action<int> BalanceChanged = delegate { };
}
