using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependencyInjector
{
    void InjectDependencies(Transform t);
}
