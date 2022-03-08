using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DependencyInjector<T> : MonoBehaviour, IDependencyInjector where T : Component
{
    [SerializeField] T DependencyToInject; 

    public void InjectDependencies(Transform dependent)
    {
        Dependency<T>[] ts = dependent.GetComponents<Dependency<T>>();

        foreach (Dependency<T> t in ts)
        {
            t.InjectDependency(DependencyToInject);
        }
    }
}
