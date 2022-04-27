using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectSCChildren : MonoBehaviour, Initializable
{
    public void Init()
    {
        InjectStateController sc = GetComponent<InjectStateController>();

        foreach (Transform t in transform)
        {
            sc.InjectDependencies(t);
        }
    }
}
