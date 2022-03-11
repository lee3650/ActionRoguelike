using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTalentManager
{
    TalentManager tm;

    EmptyTalent[] talents;

    const int numTalents = 10;

    [SetUp]
    public void Setup()
    {
        tm = GetEmptyObject.GetEmpty().AddComponent<TalentManager>();

        talents = new EmptyTalent[numTalents];

        for (int i = 0; i < numTalents; i++)
        {
            talents[i] = GetEmptyObject.GetEmpty().AddComponent<EmptyTalent>();
        }
    }

    [Test]
    public void TestTalentManagerSetsHighestTalentToZero()
    {
        tm.Init();
        Assert.AreEqual(0, tm.GetNumberOfActiveTalents());
    }

    [Test]
    public void TestTalentManagerSetsHighestTalent()
    {
        tm.SetTalentSlot(3, talents[0]);
        tm.Init();
        Assert.AreEqual(4, tm.GetNumberOfActiveTalents());
    }

    [Test]
    public void TestTalentManagerAddTalent()
    {
        tm.Init();
        tm.AddTalent(talents[0]);
        tm.AddTalent(talents[1]);
        Assert.AreEqual(2, tm.GetNumberOfActiveTalents());
    }

    [Test]
    public void TestAddManyTalents()
    {
        tm.Init();
        for (int i = 0; i < numTalents; i++)
        {
            tm.AddTalent(talents[i]);
        }
    }

    [Test]
    public void TestTalentValid()
    {
        tm.Init();
        Assert.AreEqual(false, tm.IsActiveTalentValid(0));

        tm.AddTalent(talents[0]);

        Assert.AreEqual(true, tm.IsActiveTalentValid(0));
    }

    [Test]
    public void TestTalentAllowed()
    {
        tm.Init();
        talents[0].useable = true;
        talents[1].useable = false;

        tm.AddTalent(talents[0]);
        tm.AddTalent(talents[1]);

        Assert.IsTrue(tm.IsTalentAllowed(0));
        Assert.IsFalse(tm.IsTalentAllowed(1));
    }
}
