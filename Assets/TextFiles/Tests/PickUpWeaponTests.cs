using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PickUpWeaponTests
{
    TestClassToInject testDependency;
    TestWeapon weapon;
    PickUpWeapon pickUp;
    TestWeaponManager weaponManager;

    [SetUp]
    public void Setup()
    {
        GameObject inventory = GetEmptyObject.GetEmpty();
        GameObject weaponGameObject = GetEmptyObject.GetEmpty();
        TestClassInjector testInjector = inventory.AddComponent<TestClassInjector>();
        testDependency = inventory.AddComponent<TestClassToInject>();

        testInjector.SetTestClassToInject(testDependency);

        weapon = weaponGameObject.AddComponent<TestWeapon>();
        pickUp = inventory.AddComponent<PickUpWeapon>();

        GameObject wm = GetEmptyObject.GetEmpty();
        weaponManager = wm.AddComponent<TestWeaponManager>();

        pickUp.SetDefaultWeapon(weapon);
        pickUp.SetInjectors(new Component[] { testInjector });
        pickUp.SetWeaponManager(weaponManager);

        pickUp.LateInit();
    }

    [Test]
    public void TestPickUpWeaponDefaultWeapon()
    {
        //make sure dependencies are set up on the default weapon
        Assert.AreEqual(testDependency, weapon.Dependency);
        //make sure the default weapon is selected 
        Assert.AreEqual(weapon, weaponManager.GetCurrentWeapon());
    }

    [Test]
    public void TestPickUpWeaponAddToInventory()
    {
        TestWeapon secondWeapon = GetEmptyObject.GetEmpty().AddComponent<TestWeapon>();

        pickUp.AddToInventory(secondWeapon);

        //make sure dependencies are set up on the new weapon
        Assert.AreEqual(testDependency, secondWeapon.Dependency);
        //make sure the default weapon is still selected 
        Assert.AreEqual(weapon, weaponManager.GetCurrentWeapon());
    }

    [Test]
    public void TestChangeSelection()
    {
        TestWeapon secondWeapon = GetEmptyObject.GetEmpty().AddComponent<TestWeapon>();

        pickUp.AddToInventory(secondWeapon);

        //make sure selected weapon is unchanged
        Assert.AreEqual(weapon, weaponManager.GetCurrentWeapon());

        pickUp.ChangeSelection(1);

        //make sure selected weapon changed
        Assert.AreEqual(secondWeapon, weaponManager.GetCurrentWeapon());
    }
}
