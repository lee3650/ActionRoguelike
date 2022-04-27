using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WielderSupplier : MonoBehaviour, Dependency<GenericTarget>, Initializable
{
    [SerializeField] private GenericTarget MyTarget;
    [SerializeField] bool UseGetComponent;

    private Targetable actualTarget;

    public void Init()
    {
        if (UseGetComponent)
        {
            actualTarget = GetComponent<Targetable>();
        }
        else
        {
            if (actualTarget == null)
            {
                actualTarget = MyTarget;
            }
        }
    }

    public Targetable GetWielder()
    {
        return actualTarget; 
    }

    public void InjectDependency(GenericTarget t)
    {
        actualTarget = t;
    }
}
