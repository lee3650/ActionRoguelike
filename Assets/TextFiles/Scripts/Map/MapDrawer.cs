using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrawer : MonoBehaviour
{
    [SerializeField] ColorMapper ColorMapper;
    private List<GameObject> currentMap = new List<GameObject>();

    public void DrawSingleMap(Color32[,] map, Vector2Int origin)
    {
        int xSize = map.GetLength(0);
        int ySize = map.GetLength(1);

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject g = Instantiate(ColorMapper.GetPrefabFromColor(map[x, y]), (Vector2)(new Vector2Int(x, y) + origin), Quaternion.identity);
                currentMap.Add(g);
            }
        }
    }
}
