using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestHealthManager
{
    HealthManager hm;
    bool tookDamage;
    bool healthChanged;
    bool died;

    const float maxHealth = 10f;

    [SetUp]
    public void Setup()
    {
        hm = GetEmptyObject.GetEmpty().AddComponent<HealthManager>();
        hm.DamageTaken += DamageTaken;
        hm.OnDeath += OnDeath;
        hm.HealthChanged += HealthChanged;

        hm.SetMaxHealth(maxHealth);

        hm.Init();

        tookDamage = false;
        healthChanged = false;
        died = false; 
    }

    private void HealthChanged()
    {
        healthChanged = true;
    }

    private void OnDeath()
    {
        died = true; 
    }

    private void DamageTaken()
    {
        tookDamage = true;
    }

    [Test]
    public void TestTookDamage()
    {
        hm.TakeDamage(1);
        Assert.IsTrue(tookDamage);
        Assert.IsTrue(hm.IsAlive());
        Assert.IsFalse(died);
        Assert.IsTrue(healthChanged);
    }

    [Test]
    public void TestDied()
    {
        hm.TakeDamage(10);
        Assert.IsTrue(tookDamage);
        Assert.IsTrue(died);
        Assert.IsTrue(healthChanged);
        Assert.IsFalse(hm.IsAlive());
    }
}
