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
        if (col.a == 0)
        {
            return null;
        }

        Prereq.Assert(ColorMap.ContainsKey(col), string.Format("Did not have a value for key {0}", col));

        return ColorMap[col].Prefab;
    }

    public bool GetTraversableFromColor(Color32 col)
    {
        if (col.a == 0)
        {
            return false; 
        }

        Prereq.Assert(ColorMap.ContainsKey(col), string.Format("Did not have a value for key {0}", col));

        return ColorMap[col].Traversable;
    }

    public static readonly Color32 DoorColor = new Color32(120, 120, 0, 255);
}
