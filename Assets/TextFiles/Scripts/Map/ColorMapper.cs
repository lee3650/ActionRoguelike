using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMapper : MonoBehaviour, Initializable
{
    [SerializeField] private ColorMapEntry[] colorMap;

    private Dictionary<Color32, ColorMapData> ColorMap = new Dictionary<Color32, ColorMapData>();

    public void Init()
    {
        foreach (ColorMapEntry c in colorMap)
        {
            ColorMap[c.Color] = c.Data;
        }
    }

    public GameObject GetPrefabFromColor(Color32 col)
    {
        return ColorMap[col].Prefab;
    }
}
