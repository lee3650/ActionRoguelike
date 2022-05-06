using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRoomChooser : RoomChooser
{
    [SerializeField] List<RoomData> Rooms;
    public override List<RoomData> ChooseRandomRooms()
    {
        return RoomDataSupplier.CopyList(Rooms);
    }
}
