using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WielderSupplier : MonoBehaviour, Dependency<GenericTarget>
{
    private GenericTarget MyTarget; 

    public Targetable GetWielder()
    {
        return MyTarget; 
    }

    public void InjectDependency(GenericTarget t)
    {

    }
}
