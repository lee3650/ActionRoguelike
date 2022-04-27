using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClassDependent : MonoBehaviour, Dependency<TestClassToInject>
{
    public TestClassToInject Dependency
    {
        get;
        private set; 
    }

    public void InjectDependency(TestClassToInject t)
    {
        Dependency = t; 
    }
}
