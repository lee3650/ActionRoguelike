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
        float dir = KeyboardInput.GetRotationFromDirection(testVector);
        Assert.AreEqual(0f, dir, 0.001f);

        testVector = new Vector2(1, -1);
        dir = KeyboardInput.GetRotationFromDirection(testVector);
        Assert.AreEqual(315, (dir + 360) % 360, 0.001f);

        testVector = new Vector2(-1, 1);
        dir = KeyboardInput.GetRotationFromDirection(testVector);
        Assert.AreEqual(135f, dir, 0.001f);

        testVector = new Vector2(-1, 0);
        dir = KeyboardInput.GetRotationFromDirection(testVector);
        Assert.AreEqual(180f, (dir + 360) % 360, 0.001f);

        testVector = new Vector2(-1, -1);
        dir = KeyboardInput.GetRotationFromDirection(testVector);
        Assert.AreEqual(225f, (dir + 360) % 360, 0.001f);
    }
}
