using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomChooser : MonoBehaviour
{
    /// <summary>
    /// These rooms will be placed in order, rooms[0] is the starting room
    /// </summary>
    public abstract List<RoomData> ChooseRandomRooms();

    public abstract RoomData GetBossRoom();
}
