using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoomDrawer : MonoBehaviour
{
    [SerializeField] MapDrawer MapDrawer;
    [SerializeField] ColorMapper ColorMapper;
    [SerializeField] EnemyColorMapper EnemyMapper;
    [SerializeField] Room RoomPrefab; 

    public void DrawRoom(RoomData roomdata, Room currentRoom, Color32[,] room)
    {
        if (roomdata.DestructiveEnv != null)
        {
            Color32[,] overwrite = TextureReader.ReadSprite(roomdata.DestructiveEnv);
            OverwriteTiles(room, overwrite);
            SpawnRoomChildren(overwrite, roomdata.Offset, currentRoom);
        }

        if (roomdata.AdditiveEnv != null)
        {
            Color32[,] nondestr = TextureReader.ReadSprite(roomdata.AdditiveEnv);
            SpawnRoomChildren(nondestr, roomdata.Offset, currentRoom);
        }

        MapDrawer.DrawSingleMap(room, roomdata.Offset);

        if (roomdata.Wave != null)
        {
            Color32[,] wave = TextureReader.ReadSprite(roomdata.Wave);
            SpawnWave(wave, roomdata.Offset, currentRoom);
        }

        currentRoom.LateInit();
    }

    public void OverwriteTiles(Color32[,] room, Color32[,] overwrite)
    {
        for (int x = 0; x < room.GetLength(0); x++)
        {
            for (int y = 0; y < room.GetLength(1); y++)
            {
                if (overwrite[x, y].a != 0)
                {
                    room[x, y] = new Color32(0, 0, 0, 0);
                }
            }
        }
    }

    public void SpawnRoomChildren(Color32[,] traps, Vector2Int offset, Room r)
    {
        for (int x = 0; x < traps.GetLength(0); x++)
        {
            for (int y = 0; y < traps.GetLength(1); y++)
            {
                if (traps[x, y].a > 0)
                {
                    GameObject child = ColorMapper.GetPrefabFromColor(traps[x, y]);

                    if (child != null)
                    {
                        GameObject t = Instantiate(child, (Vector2)(offset + MapDrawer.TileSize * new Vector2Int(x, y)), Quaternion.identity);
                        if (t.GetComponent<RoomChild>())
                        {
                            r.AddRoomChild(t.GetComponent<RoomChild>());
                        }
                    }
                }
            }
        }
    }

    public Vector3 TranslateWorld(int x, int y, Vector2Int offset)
    {
        return (Vector2)(offset + MapDrawer.TileSize * new Vector2Int(x, y));
    }

    public void SpawnWave(Color32[,] wave, Vector2Int offset, Room r)
    {
        for (int x = 0; x < wave.GetLength(0); x++)
        {
            for (int y = 0; y < wave.GetLength(1); y++)
            {
                GetHealthManager hm = EnemyMapper.GetEntryFromColor(wave[x, y]);
                
                if (hm != null)
                {
                    GetHealthManager enemy = Instantiate(hm, (Vector2)(offset + MapDrawer.TileSize * new Vector2Int(x, y)), Quaternion.identity);
                    r.AddEnemy(enemy.HealthManager);
                }
            }
        }
    }

    public void WriteTextureToMap(Color32[,] tex, Vector2Int offset)
    {
        for (int x = 0; x < tex.GetLength(0); x++)
        {
            for (int y = 0; y < tex.GetLength(1); y++)
            {
                TraverseManager.SetPoint(MapDrawer.TileSize * x + offset.x, MapDrawer.TileSize * y + offset.y, ColorMapper.GetTraversableFromColor(tex[x,y]));
            }
        }
    }

    public void WriteRoomToMap(Color32[,] tex, Vector2Int offset, Room r)
    {
        for (int x = 0; x < tex.GetLength(0); x++)
        {
            for (int y = 0; y < tex.GetLength(1); y++)
            {
                Vector2Int pos = new Vector2Int(MapDrawer.TileSize * x + offset.x, MapDrawer.TileSize * y + offset.y);
                if (tex[x, y].a > 0)
                {
                    TraverseManager.SetRoom(pos.x, pos.y, r);
                } else
                {
                    TraverseManager.SetRoom(pos.x, pos.y, null);
                }
            }
        }
    }
}
