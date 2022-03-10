using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAndRemoveTarget : MonoBehaviour, Initializable, LateInitializable
{
    private Targetable MyTarget; 

    public void Init()
    {
        MyTarget = GetComponent<Targetable>();
        print("getting targetable! Targetable is " + MyTarget);
    }

    //not sure about that one 
    public void LateInit()
    {
        if (MyTarget == null)
        {
            throw new System.Exception("my target was null!");
        }
        TargetManager.AddTargetable(MyTarget);
    }

    public void RemoveTarget()
    {
        TargetManager.RemoveTarget(MyTarget);
    }
}
