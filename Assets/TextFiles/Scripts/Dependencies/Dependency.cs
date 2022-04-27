using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Dependency<T>
{
    void InjectDependency(T dependency);
}
