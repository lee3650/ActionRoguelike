using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectControllerOnSecondInit : MonoBehaviour, Initializable
{
    public void Init()
    {
        GetComponent<InjectStateController>().InjectDependencies(this.transform);
    }
}
