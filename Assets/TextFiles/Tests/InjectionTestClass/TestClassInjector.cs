using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClassInjector : DependencyInjector<TestClassToInject>
{
    public void SetTestClassToInject(TestClassToInject t)
    {
        DependencyToInject = t; 
    }
}
