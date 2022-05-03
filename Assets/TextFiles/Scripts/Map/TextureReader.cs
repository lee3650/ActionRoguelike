using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureReader : MonoBehaviour
{
    public static Color32[,] ReadTexture(Texture2D tex)
    {
        Prereq.Assert(tex != null, "Texture was null");

        Color32[] flat = tex.GetPixels32();

        return UnwrapArray(flat, tex.width, tex.height);
    }

    private static Color32[,] UnwrapArray(Color32[] flat, int width, int height)
    {
        Color32[,] result = new Color32[width, height];

        for (int i = 0; i < flat.Length; i++)
        {
            result[i % width, i / width] = flat[i];
        }

        return result; 
    }

    public static Color32[,] ReadSprite(Sprite sprite)
    {
        Prereq.Assert(sprite != null, "Sprite was null");

        int width = Mathf.RoundToInt(sprite.rect.width);
        int height = Mathf.RoundToInt(sprite.rect.height);

        int x = Mathf.RoundToInt(sprite.rect.x);
        int y = Mathf.RoundToInt(sprite.rect.y);

        Color[] flat_float = sprite.texture.GetPixels(x, y, width, height);

        Color32[] flat = new Color32[flat_float.Length];

        for (int i = 0; i < flat_float.Length; i++)
        {
            flat[i] = NormalizeColor(flat_float[i]);
        }

        return UnwrapArray(flat, width, height);
    }

    private static Color32 NormalizeColor(Color c)
    {
        return new Color32(ChannelToInt(c.r), ChannelToInt(c.g), ChannelToInt(c.b), ChannelToInt(c.a));
    }

    private static byte ChannelToInt(float feature)
    {
        return (byte)Mathf.RoundToInt(255f * feature);
    }
}
