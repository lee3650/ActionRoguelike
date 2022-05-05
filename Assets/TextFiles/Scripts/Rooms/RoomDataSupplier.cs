using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomDataSupplier : MonoBehaviour
{
    public abstract List<RoomData> ChooseRooms();

    public static List<RoomData> CopyList(List<RoomData> data)
    {
        List<RoomData> result = new List<RoomData>();

        foreach (RoomData o in data)
        {
            result.Add(RoomData.Copy(o));
        }

        return result; 
    }
}
