using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingleRoomPlacer
{
    public const int MAX_BRANCH_LENGTH = 4;

    public static (bool success, Vector2Int offset, int branch_length) PlaceRoom(int branchLength, Vector2Int[] Directions, RoomData cur, List<RoomData> ExistingRooms)
    {
        while (true)
        {
            if (branchLength < MAX_BRANCH_LENGTH)
            {
                for (int j = 0; j < Directions.Length; j++)
                {
                    Vector2Int dir = Directions[j];

                    //can you move in direction "dir" from the parent and from direction "-dir" in the child? 
                    (bool success, Vector2Int pos) parentresult = OffsetCalculator.GetDoorPosition(cur.Parent, dir);
                    (bool success, Vector2Int pos) result = OffsetCalculator.GetDoorPosition(cur, -dir);

                    if (result.success && parentresult.success)
                    {
                        Vector2Int offset = OffsetCalculator.GetOffset(cur.Parent, parentresult.pos, result.pos, dir);
                        if (positionAvailable(cur, offset, ExistingRooms))
                        {
                            branchLength++;
                            return (true, offset, branchLength);
                        }
                    }
                }
            }

            cur.Parent = cur.Parent.Parent;

            branchLength--;

            if (cur.Parent == null)
            {
                MonoBehaviour.print("ending early!");
                return (false, new Vector2Int(), branchLength);
            }
        }
    }

    private static bool positionAvailable(RoomData room, Vector2Int pos, List<RoomData> ExistingRooms)
    {
        Vector2Int[] corners = GetCorners(room, pos);

        foreach (RoomData r in ExistingRooms)
        {
            Vector2Int[] existingCorners = GetCorners(r, r.Offset);

            for (int i = 0; i < corners.Length; i++)
            {
                if (PointInRoom(r, r.Offset, corners[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < existingCorners.Length; i++)
            {
                if (PointInRoom(room, pos, existingCorners[i]))
                {
                    MonoBehaviour.print(string.Format("Point {0} was in the newly placed room with position {1} and bounds {2}, {3}", 
                        existingCorners[i], pos, room.XSize, room.YSize));
                    return false;
                }
            }
        }

        return true;
    }

    private static Vector2Int[] GetCorners(RoomData room, Vector2Int pos)
    {
        return new Vector2Int[]
        {
            pos,
            MapDrawer.TileSize * new Vector2Int(0, room.YSize - 1) + pos,
            MapDrawer.TileSize * new Vector2Int(room.XSize - 1, 0) + pos,
            MapDrawer.TileSize * new Vector2Int(room.XSize - 1, room.YSize - 1) + pos,
        };
    }

    private static bool PointInRoom(RoomData room, Vector2Int offset, Vector2Int point)
    {
        return point.x >= offset.x && point.x < offset.x + (MapDrawer.TileSize * room.XSize)
            && point.y >= offset.y && point.y < offset.y + (MapDrawer.TileSize * room.YSize);
    }
}
