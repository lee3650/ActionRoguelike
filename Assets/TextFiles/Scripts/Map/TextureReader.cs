using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureReader : MonoBehaviour
{
    public static Color32[,] ReadTexture(Texture2D tex)
    {
        Prereq.Assert(tex != null, "Texture was null");

        Color32[,] result = new Color32[tex.width, tex.height];
        
        Color32[] flat = tex.GetPixels32();

        for (int i = 0; i < flat.Length; i++)
        {
            result[i % tex.width, i / tex.width] = flat[i];
        }

        return result; 
    }
}
