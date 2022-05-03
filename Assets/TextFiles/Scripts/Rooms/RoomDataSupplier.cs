using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomDataSupplier : MonoBehaviour
{
    public abstract List<RoomData> ChooseRooms();
}
