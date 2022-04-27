using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionSet : MonoBehaviour, Initializable
{
    [SerializeField] Component[] injectors;

    private List<IDependencyInjector> Injectors = new List<IDependencyInjector>(); 

    public void SetInjectors(Component[] injectors)
    {
        this.injectors = injectors; 
    }

    public void InjectDependencies(Transform t)
    {
        foreach (IDependencyInjector i in Injectors)
        {
            i.InjectDependencies(t);
        }
    }

    public void Init()
    {
        foreach (Component c in injectors)
        {
            Injectors.Add(c as IDependencyInjector);
        }
    }
}
