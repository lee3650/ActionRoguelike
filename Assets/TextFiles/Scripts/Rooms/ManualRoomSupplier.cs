using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRoomSupplier : RoomDataSupplier
{
    [SerializeField] List<RoomData> roomDatas;
    public override List<RoomData> ChooseRooms()
    {
        return CopyList(roomDatas);
    }
}
