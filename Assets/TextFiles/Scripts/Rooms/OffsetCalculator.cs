using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetCalculator : MonoBehaviour
{
    public static Vector2Int GetOffset(RoomData parent, RoomData child, Vector2Int parentDoor, Vector2Int childDoor, Vector2Int direction)
    {
        Prereq.Assert(direction.magnitude == 1, "Direction did not have a magnitude of 1");

        Vector2Int parentDoorWorld = parent.Offset + (parentDoor * MapDrawer.TileSize);
        Vector2Int childWorldPos = parentDoorWorld + (direction * MapDrawer.TileSize);
        Vector2Int offset = childWorldPos - (childDoor * MapDrawer.TileSize);

        return offset; 
    }

    public static Vector2Int GetDoorPosition(RoomData room, Vector2Int direction)
    {
        Prereq.Assert(direction.magnitude == 1, "Direction did not have a magnitude of 1");

        Color32[,] tiles = TextureReader.ReadSprite(room.Room);

        Vector2Int searchDir = new Vector2Int(1, 0);

        Vector2Int start = searchDir * (GetSizeInDirection(searchDir, room) - 1);

        int count = 4; 
        while (searchDir != direction)
        {
            Prereq.Assert(count >= 0, "Could not wrap direction around! Direction: " + direction);
            count--;

            start += searchDir * GetSizeInDirection(searchDir, room);

            searchDir = GetOrthog(searchDir);
        }
    }

    private static int GetSizeInDirection(Vector2Int direction, RoomData room)
    {
        return Mathf.Max(Mathf.Abs(direction.x * room.XSize), Mathf.Abs(direction.y * room.YSize));
    }

    private static Vector2Int GetOrthog(Vector2Int dir)
    {
        return new Vector2Int(-dir.y, dir.x);
    }
}
