using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InjectionTests
{
    private GameObject empty; 

    [SetUp]
    public void Setup()
    {
        empty = GetEmptyObject.GetEmpty();
    }

    [Test]
    public void InjectionTestsSimplePasses()
    {
        TestClassToInject t = empty.AddComponent<TestClassToInject>();
        TestClassInjector injector = empty.AddComponent<TestClassInjector>();
        injector.SetTestClassToInject(t);
        TestClassDependent dependent = empty.AddComponent<TestClassDependent>();
        injector.InjectDependencies(empty.transform);
        Assert.AreEqual(t, dependent.Dependency);
    }
}
