using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RotationTests
{
    [Test]
    public void TestKeyboardInputRotation()
    {
        Vector2 testVector = new Vector2(1, 0);
        float dir = UtilityFunctions.GetRotationFromDirection(testVector);
        Assert.AreEqual(0f, dir, 0.001f);

        testVector = new Vector2(1, -1);
        dir = UtilityFunctions.GetRotationFromDirection(testVector);
        Assert.AreEqual(315, (dir + 360) % 360, 0.001f);

        testVector = new Vector2(-1, 1);
        dir = UtilityFunctions.GetRotationFromDirection(testVector);
        Assert.AreEqual(135f, dir, 0.001f);

        testVector = new Vector2(-1, 0);
        dir = UtilityFunctions.GetRotationFromDirection(testVector);
        Assert.AreEqual(180f, (dir + 360) % 360, 0.001f);

        testVector = new Vector2(-1, -1);
        dir = UtilityFunctions.GetRotationFromDirection(testVector);
        Assert.AreEqual(225f, (dir + 360) % 360, 0.001f);
    }

    [Test]
    public void TestDirectionFromRotation()
    {
        float testRot = 0f;
        Vector2 dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(1, 0));

        testRot = 90f;
        dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(0, 1));

        testRot = -90f;
        dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(0, -1));

        testRot = 180f;
        dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(-1, 0));

        testRot = 270f;
        dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(0, -1));

        testRot = 360;
        dir = UtilityFunctions.GetDirectionFromRotation(testRot);
        Assert.IsTrue(dir == new Vector2(1, 0));
    }
}
