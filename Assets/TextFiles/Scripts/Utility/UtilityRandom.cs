using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityRandom : MonoBehaviour
{
    public static IList SortByRandom(IList input)
    {
        for (int i = input.Count - 1; i >= 1; i--)
        {
            object temp = input[i];
            int swapindex = Random.Range(0, i + 1);
            input[i] = input[swapindex];
            input[swapindex] = temp;
        }

        return input; 
    }

    public static bool PercentChance(float chance)
    {
        return Random.Range(0f, 100f) <= chance; 
    }
}
