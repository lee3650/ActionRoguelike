using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestStatsList
{
    private StatsList stats;

    [SetUp]
    public void Setup()
    {
        stats = GetEmptyObject.GetEmpty().AddComponent<StatsList>();
        stats.Init();
    }

    [Test]
    public void TestSingleListener()
    {
        MockStatsListener m = new MockStatsListener();
        stats.RegisterListener("test", m);
        stats.SetStat("test", 99f);
        Assert.AreEqual(99f, m.Stats["test"]);
    }

    [Test]
    public void TestMultipleListeners()
    {
        List<MockStatsListener> listeners = new List<MockStatsListener>();
        
        for (int i = 0; i < 10; i++)
        {
            listeners.Add(new MockStatsListener());
            stats.RegisterListener("test", listeners[i]);
        }

        for (int i = 0; i < 5; i++)
        {
            listeners.Add(new MockStatsListener());
            stats.RegisterListener("test1", listeners[i]);
        }

        stats.SetStat("test1", 7);
        stats.SetStat("test", 10);
        stats.MultiplyStat("test", 3);
        stats.AddToStat("test", 9);
        
        for (int i = 0; i < 10; i++)
        {
            if (i < 5)
            {
                Assert.AreEqual(7, listeners[i].Stats["test1"]);
            }
            Assert.AreEqual(39, listeners[i].Stats["test"]);
        }
    }

    [Test]
    public void TestRemoveListeners()
    {
        List<MockStatsListener> listeners = new List<MockStatsListener>();

        for (int i = 0; i < 10; i++)
        {
            listeners.Add(new MockStatsListener());
            stats.RegisterListener("test", listeners[i]);
        }

        for (int i = 0; i < 5; i++)
        {
            listeners.Add(new MockStatsListener());
            stats.RegisterListener("test1", listeners[i]);
        }

        stats.SetStat("test1", 7);
        stats.SetStat("test", 10);
        stats.MultiplyStat("test", 3);
        stats.AddToStat("test", 9);

        for (int i = 0; i < 10; i++)
        {
            stats.UnregisterListener(listeners[i], "test");
        }

        stats.AddToStat("test", 100);
        
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(39, listeners[i].Stats["test"]);
        }

        stats.MultiplyStat("test1", 7);

        for (int i = 0; i < 5; i++)
        {
            Assert.AreEqual(49, listeners[i].Stats["test1"]);
        }
    }
}
