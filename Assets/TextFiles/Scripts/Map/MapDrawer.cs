using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrawer : MonoBehaviour
{
    public const int TileSize = 2; 

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
                GameObject pref = ColorMapper.GetPrefabFromColor(map[x, y]);
                if (pref != null)
                {
                    Vector2 pos = (Vector2)(TileSize * new Vector2Int(x, y) + origin);
                    Vector3 pos3 = new Vector3(pos.x, pos.y, pref.transform.position.z);
                    GameObject g = Instantiate(pref, pos3, Quaternion.identity);
                    currentMap.Add(g);
                }
            }
        }
    }
}
