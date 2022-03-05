using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversedTracker : MonoBehaviour
{
    [SerializeField] bool reversed = false;

    public bool Reversed
    {
        get
        {
            return reversed; 
        }
    }

    public void ToggleReversed()
    {
        reversed = !reversed;
    }
}
