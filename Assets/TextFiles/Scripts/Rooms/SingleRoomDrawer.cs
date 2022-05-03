using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoomDrawer : MonoBehaviour, LateInitializable
{
    [SerializeField] MapDrawer MapDrawer;
    [SerializeField] bool drawDefaultRoom = false;
    [SerializeField] Sprite defaultRoom;
    [SerializeField] Sprite defaultEnemies;
    [SerializeField] Texture2D defaultDecorations;
    [SerializeField] Texture2D additiveEnv;
    [SerializeField] Sprite destructiveEnv;
    [SerializeField] Vector2Int room_offset;
    [SerializeField] ColorMapper ColorMapper;
    [SerializeField] EnemyColorMapper EnemyMapper;
    [SerializeField] Room RoomPrefab; 

    public void LateInit()
    {
        if (!drawDefaultRoom)
        {
            return; 
        }

        print("~~~~~~~~~~~~~~drawing map!~~~~~~~~~~~~~~");

        Color32[,] room = TextureReader.ReadSprite(defaultRoom);

        TraverseManager.Initialize(room.GetLength(0), room.GetLength(1), room_offset, MapDrawer.TileSize);

        WriteTextureToMap(room, room_offset);

        Room currentRoom = Instantiate<Room>(RoomPrefab);

        WriteRoomToMap(room, room_offset, currentRoom);

        if (destructiveEnv != null)
        {
            Color32[,] overwrite = TextureReader.ReadSprite(destructiveEnv);
            OverwriteTiles(room, overwrite);
            SpawnRoomChildren(overwrite, room_offset, currentRoom);
        }

        if (additiveEnv != null)
        {
            Color32[,] nondestr = TextureReader.ReadTexture(additiveEnv);
            SpawnRoomChildren(nondestr, room_offset, currentRoom);
        }

        MapDrawer.DrawSingleMap(room, room_offset);

        Color32[,] wave = TextureReader.ReadSprite(defaultEnemies);

        SpawnWave(wave, room_offset, currentRoom);

        currentRoom.LateInit();
    }

    public void DrawRoom(RoomData roomdata)
    {
        Color32[,] room = TextureReader.ReadSprite(roomdata.Room);

        WriteTextureToMap(room, roomdata.Offset);

        Room currentRoom = Instantiate<Room>(RoomPrefab);

        WriteRoomToMap(room, roomdata.Offset, currentRoom);

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

        Color32[,] wave = TextureReader.ReadSprite(roomdata.Wave);

        SpawnWave(wave, roomdata.Offset, currentRoom);

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
