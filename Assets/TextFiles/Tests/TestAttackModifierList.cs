using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAttackModifierList
{
    AttackModifierList list; 

    [SetUp]
    public void Setup()
    {
        list = GetEmptyObject.GetEmpty().AddComponent<AttackModifierList>();
    }

    [Test]
    public void TestAddModifier()
    {
        list.AddAttackModifier(new GameEvent(SignalType.Fire, 10, null, new StatDictionary()));
        Assert.AreEqual(1, list.GetAttackModifiers().Count);
        Assert.IsTrue(list.GetAttackModifiers()[0].Magnitude == 10 && list.GetAttackModifiers()[0].Type == SignalType.Fire);
    }

    [Test]
    public void TestRemoveModifier()
    {
        GameEvent e = new GameEvent(SignalType.Fire, 10, null, new StatDictionary());

        list.AddAttackModifier(e);

        Assert.AreEqual(1, list.GetAttackModifiers().Count);
        Assert.IsTrue(list.GetAttackModifiers()[0].Magnitude == 10 && list.GetAttackModifiers()[0].Type == SignalType.Fire);

        list.RemoveAttackModifier(e);
        Assert.AreEqual(0, list.GetAttackModifiers().Count);
    }
}
