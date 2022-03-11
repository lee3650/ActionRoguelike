using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon, Dependency<TestClassToInject>
{
    public TestClassToInject Dependency; 

    public void InjectDependency(TestClassToInject t)
    {
        Dependency = t; 
    }

    public override bool ActionAllowed(string action)
    {
        return true; 
    }

    public override bool ActionFinished()
    {
        return true; 
    }

    public override void Deselect()
    {

    }

    public override void Select()
    {

    }

    public override void StartAction(string action)
    {

    }
}
