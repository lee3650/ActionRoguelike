using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : RoomDataSupplier
{
    [SerializeField] RoomChooser RoomChooser;

    private List<RoomData> ExistingRooms = new List<RoomData>();

    public void SetRoomChooser(RoomChooser chooser)
    {
        RoomChooser = chooser; 
    }

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

            (bool success, Vector2Int offset, int branch_length) = SingleRoomPlacer.PlaceRoom(branchLength, Directions, cur, ExistingRooms);

            if (success)
            {
                branchLength = branch_length;
                parent = cur;
                cur.Offset = offset; 
                ExistingRooms.Add(cur);
            }

        }

        return ExistingRooms; 
    }
}
