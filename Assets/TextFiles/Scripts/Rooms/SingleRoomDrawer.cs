using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoomDrawer : MonoBehaviour, LateInitializable
{
    [SerializeField] MapDrawer MapDrawer;
    [SerializeField] bool drawDefaultRoom = false;
    [SerializeField] Texture2D defaultRoom;
    [SerializeField] Texture2D defaultEnemies;
    [SerializeField] Texture2D defaultDecorations;
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

        Color32[,] room = TextureReader.ReadTexture(defaultRoom);

        TraverseManager.Initialize(room.GetLength(0), room.GetLength(1), room_offset, MapDrawer.TileSize);

        WriteTextureToMap(room, room_offset);

        Room currentRoom = Instantiate<Room>(RoomPrefab);

        WriteRoomToMap(room, room_offset, currentRoom);

        MapDrawer.DrawSingleMap(room, room_offset);

        Color32[,] wave = TextureReader.ReadTexture(defaultEnemies);

        SpawnWave(wave, room_offset, currentRoom);

        currentRoom.LateInit();
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
