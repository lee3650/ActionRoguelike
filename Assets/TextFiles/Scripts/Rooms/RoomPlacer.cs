using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : RoomDataSupplier
{
    [SerializeField] RoomChooser RoomChooser;

    private List<RoomData> ExistingRooms = new List<RoomData>();

    Vector2Int[] Directions = new Vector2Int[]
    {
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
    };

    public override List<RoomData> ChooseRooms()
    {
        ExistingRooms = new List<RoomData>();

        int branchLength = 0;

        //It's better to do this up front because we need to guarantee certain rooms (chest/boss) appear
        List<RoomData> roomsToPlace = RoomChooser.ChooseRandomRooms();

        Prereq.Assert(roomsToPlace.Count > 0, "There were no rooms chosen!");

        ExistingRooms.Add(roomsToPlace[0]);

        RoomData parent = roomsToPlace[0];

        //start at 1 because we already placed the parent
        for (int i = 1; i < roomsToPlace.Count; i++)
        {
            Directions = (Vector2Int[])UtilityRandom.SortByRandom(Directions);

            RoomData cur = roomsToPlace[i];
            cur.Parent = parent;

            bool placed = false;

            while (!placed)
            {
                if (branchLength < 4)
                {
                    for (int j = 0; j < Directions.Length; j++)
                    {
                        Vector2Int dir = Directions[j];
                        (bool success, Vector2Int pos) parentresult = OffsetCalculator.GetDoorPosition(cur.Parent, dir);
                        (bool success, Vector2Int pos) result = OffsetCalculator.GetDoorPosition(cur, -dir);
                        if (result.success && parentresult.success)
                        {
                            Vector2Int offset = OffsetCalculator.GetOffset(cur.Parent, parentresult.pos, result.pos, dir);
                            if (positionAvailable(cur, offset))
                            {
                                branchLength++;
                                cur.Offset = offset;
                                parent = cur;
                                placed = true;
                                ExistingRooms.Add(cur);
                                break;
                            }
                        }
                    }
                }

                parent = parent.Parent;
                cur.Parent = parent;

                branchLength--;

                if (parent == null)
                {
                    //I guess we're just done at this point
                    print("ending early!");
                    return ExistingRooms; 
                    //throw new System.Exception("Unable to place module!");
                }
            }
        }

        return ExistingRooms; 
    }

    private bool positionAvailable(RoomData room, Vector2Int pos)
    {
        foreach (RoomData r in ExistingRooms)
        {
            Vector2Int[] corners = new Vector2Int[]
            {
                pos, 
                MapDrawer.TileSize * new Vector2Int(0, room.YSize) + pos,
                MapDrawer.TileSize * new Vector2Int(room.XSize, 0) + pos,
                MapDrawer.TileSize * new Vector2Int(room.XSize, room.YSize) + pos,
            };

            for (int i = 0; i < corners.Length; i++)
            {
                if (PointInRoom(r, corners[i]))
                {
                    return false; 
                }
            }
        }

        return true; 
    }

    private bool PointInRoom(RoomData room, Vector2Int point)
    {
        return point.x >= room.Offset.x && point.x < room.Offset.x + (MapDrawer.TileSize * room.XSize)
            && point.y >= room.Offset.y && point.y < room.Offset.y + (MapDrawer.TileSize * room.YSize);
    }
}
