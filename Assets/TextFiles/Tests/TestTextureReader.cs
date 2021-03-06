using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTextureReader
{
    [Test]
    public void TestTextureReaderUnwrapping()
    {
        Color32[,] expected = new Color32[,]
        {
            { new Color32(255, 0, 0, 255), new Color32(125, 125, 216, 255), },
            { new Color32(0, 255, 0, 255), new Color32(24, 116, 18, 255), },
            { new Color32(0, 0, 255, 255), new Color32(0, 0, 0, 0), },
            { new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 0), },
        };

        Assert.AreEqual(4, expected.GetLength(0));
        Assert.AreEqual(2, expected.GetLength(1));

        Texture2D tex = Resources.Load<Texture2D>("TestResources/test_texture");

        Color32[,] result = TextureReader.ReadTexture(tex);

        CompareColorArrays(expected, result);
    }

    [Test]
    public void TestSpriteReader()
    {
        Color32[,] expected1 = new Color32[,]
        {
            { new Color32(0, 0, 0, 0), new Color32(0, 255, 77, 255) },
            { new Color32(0, 255, 77, 255), new Color32(0, 0, 0, 0) },
            { new Color32(0, 0, 0, 0), new Color32(0, 155, 255, 255) },
            { new Color32(0, 155, 255, 255), new Color32(0, 0, 0, 0) },
        };

        Sprite sprite1 = Resources.Load<SpriteRenderer>("TestResources/TestSprite2").sprite;

        Color32[,] result1 = TextureReader.ReadSprite(sprite1);

        CompareColorArrays(expected1, result1);

        Color32[,] expected2 = new Color32[,]
        {
            { new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 0) },
            { new Color32(0, 77, 255, 255), new Color32(255, 0, 0, 255) },
            { new Color32(255, 255, 255, 255), new Color32(0, 0, 0, 0) },
            { new Color32(0, 0, 0, 0), new Color32(255, 124, 179, 255) },
        };

        Sprite sprite2 = Resources.Load<SpriteRenderer>("TestResources/TestSprite1").sprite;

        Color32[,] result2 = TextureReader.ReadSprite(sprite2);

        CompareColorArrays(expected2, result2);
    }

    public void CompareColorArrays(Color32[,] expected, Color32[,] actual)
    {
        Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
        Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));

        for (int x = 0; x < expected.GetLength(0); x++)
        {
            for (int y = 0; y < expected.GetLength(1); y++)
            {
                Assert.IsTrue(expected[x, y].Equals(actual[x, y]) || expected[x, y].a == 0 && actual[x, y].a == 0);
            }
        }
    }
}
