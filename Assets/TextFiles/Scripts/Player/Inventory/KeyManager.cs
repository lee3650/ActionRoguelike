using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private int keys = 0;

    public void IncrementKeys()
    {
        keys++;
    }
    public void DecrementKeys()
    {
        keys--;
    }
    public bool HasKey()
    {
        return keys > 0;
    }
}
