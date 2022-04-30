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

        for (int x = 0; x < expected.GetLength(0); x++)
        {
            for (int y = 0; y < expected.GetLength(1); y++)
            {
                Assert.IsTrue(expected[x, y].Equals(result[x, y]) || expected[x, y].a == 0 && result[x, y].a == 0);
            }
        }
    }
}
