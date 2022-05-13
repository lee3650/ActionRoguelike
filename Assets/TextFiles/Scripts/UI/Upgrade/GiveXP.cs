using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveXP : MonoBehaviour
{
    [SerializeField] float amt;

    public void ApplyXP()
    {
        XPManager.AddXP(amt);
    }
}
