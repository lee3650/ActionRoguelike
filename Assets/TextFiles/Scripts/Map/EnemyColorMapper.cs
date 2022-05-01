using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorMapper : MonoBehaviour, Initializable
{
    [SerializeField] EnemyColorMapEntry[] Entries;

    private Dictionary<Color32, GetHealthManager> Map = new Dictionary<Color32, GetHealthManager>();

    public void Init()
    {
        foreach (EnemyColorMapEntry e in Entries)
        {
            Map[e.Color] = e.Enemy;
        }
    }

    public GetHealthManager GetEntryFromColor(Color32 col)
    {
        if (col.a == 0)
        {
            return null;
        }

        if (Map.TryGetValue(col, out GetHealthManager val))
        {
            return val;
        }
        return null; 
    }
}
