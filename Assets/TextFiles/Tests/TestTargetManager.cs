using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTargetManager
{
    TargetManager tm; 

    [SetUp]
    public void Setup()
    {
        TargetManager.ResetState();

        tm = GetEmptyObject.GetEmpty().AddComponent<TargetManager>();
        
        tm.Init();
    }

    [Test]
    public void TestAddTarget()
    {
        TestTargetable tt = new TestTargetable(Factions.Enemy, true, new Vector2());
        TargetManager.AddTargetable(tt);
        Assert.AreEqual(tt, TargetManager.GetNearestTarget(new Vector2(), Factions.Player));
        Assert.IsTrue(TargetManager.GetNearestTarget(new Vector2(), Factions.Enemy) == null);
    }

    [Test]
    public void TestRemoveTarget()
    {
        TestTargetable tt = new TestTargetable(Factions.Enemy, true, new Vector2());
        TargetManager.AddTargetable(tt);
        Assert.AreEqual(tt, TargetManager.GetNearestTarget(new Vector2(), Factions.Player));
        TargetManager.RemoveTarget(tt);
        Assert.IsTrue(TargetManager.GetNearestTarget(new Vector2(), Factions.Player) == null);
    }

    [Test]
    public void TestDeadTargetsNotReturned()
    {
        TestTargetable tt = new TestTargetable(Factions.Enemy, false, new Vector2());
        TargetManager.AddTargetable(tt);
        Assert.IsTrue(TargetManager.GetNearestTarget(new Vector2(), Factions.Player) == null);
    }

    [Test]
    public void TestClosestTargetReturned()
    {
        List<TestTargetable> testTargetables = new List<TestTargetable>();

        Vector2 origin = new Vector2(0, 0);

        Vector2 dir = new Vector2(1, 1);

        for (int i = 0; i < 5; i++)
        {
            TestTargetable tt = new TestTargetable(Factions.Enemy, true, origin + (i * dir));
            testTargetables.Add(tt);
            TargetManager.AddTargetable(tt);
        }

        Assert.AreEqual(testTargetables[4], TargetManager.GetNearestTarget(new Vector2(10, 10), Factions.Player));
        Assert.AreEqual(testTargetables[0], TargetManager.GetNearestTarget(new Vector2(0, 0), Factions.Player));
    }
}
