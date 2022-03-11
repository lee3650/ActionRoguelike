using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestGenericTarget
{
    GenericTarget target;
    TestSubEntity entity; 

    [SetUp]
    public void Setup()
    {
        target = GetEmptyObject.GetEmpty().AddComponent<GenericTarget>();
        entity = target.gameObject.AddComponent<TestSubEntity>();
    }

    [Test]
    public void TestGenericTargetCallsSubEntity()
    {
        target.Init();

        GameEvent newEvent = new GameEvent(SignalType.Physical, 0, null);

        target.HandleEvent(newEvent);

        Assert.AreEqual(1, entity.handledEvents.Count);
        Assert.AreEqual(newEvent, entity.handledEvents[0]);
    }

    [Test]
    public void TestGenericTargetModifiesEvents()
    {
        TestModifyEvent modify = target.gameObject.AddComponent<TestModifyEvent>();

        target.Init();

        GameEvent newEvent = new GameEvent(SignalType.Physical, 0, null);

        target.HandleEvent(newEvent);

        Assert.AreEqual(4, entity.handledEvents.Count);
        
        foreach (GameEvent e in modify.addedEvents)
        {
            Assert.IsTrue(entity.handledEvents.Contains(e));
        }
    }
}
