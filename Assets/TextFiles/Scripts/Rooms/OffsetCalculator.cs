using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetCalculator : MonoBehaviour
{
    public static Vector2Int GetOffset(RoomData parent, Vector2Int parentDoor, Vector2Int childDoor, Vector2Int direction)
    {
        Prereq.Assert(direction.magnitude == 1, "Direction did not have a magnitude of 1");

        Vector2Int parentDoorWorld = parent.Offset + (parentDoor * MapDrawer.TileSize);
        Vector2Int childWorldPos = parentDoorWorld + (direction * MapDrawer.TileSize);
        Vector2Int offset = childWorldPos - (childDoor * MapDrawer.TileSize);

        return offset; 
    }

    public static (bool, Vector2Int) GetDoorPosition(RoomData room, Vector2Int direction)
    {
        Prereq.Assert(direction.magnitude == 1, "Direction did not have a magnitude of 1");

        Color32[,] tiles = TextureReader.ReadSprite(room.Room);

        Vector2Int start = GetSearchStart(direction, room);
        Vector2Int search_dir = GetSearchDirection(direction);

        while (InBounds(start, room))
        {
            if (ColorsEqual(tiles[start.x, start.y], ColorMapper.DoorColor))
            {
                return (true, start);
            }
            start += search_dir;
        }

        return (false, Vector2Int.zero);
    }

    private static bool ColorsEqual(Color32 a, Color32 b)
    {
        return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a; 
    }

    private static bool InBounds(Vector2Int pos, RoomData room)
    {
        return pos.x >= 0 && pos.x < room.XSize && pos.y >= 0 && pos.y < room.YSize; 
    }

    private static Vector2Int GetSearchStart(Vector2Int dir, RoomData room)
    {
        if (dir == Vector2Int.down)
        {
            return new Vector2Int(0, 0);
        }

        if (dir == Vector2Int.right)
        {
            return new Vector2Int(room.XSize - 1, 0);
        }

        if (dir == Vector2Int.up)
        {
            return new Vector2Int(0, room.YSize - 1);
        }

        if (dir == Vector2Int.left)
        {
            return new Vector2Int(0, 0);
        }

        throw new System.Exception("Did not expect input direction " + dir);
    }
    
    private static Vector2Int GetSearchDirection(Vector2Int dir)
    {
        if (dir == Vector2Int.down)
        {
            return new Vector2Int(1, 0);
        }

        if (dir == Vector2Int.right)
        {
            return new Vector2Int(0, 1);
        }

        if (dir == Vector2Int.up)
        {
            return new Vector2Int(1, 0);
        }

        if (dir == Vector2Int.left)
        {
            return new Vector2Int(0, 1);
        }

        throw new System.Exception("Did not expect input search direction " + dir);
    }
}
