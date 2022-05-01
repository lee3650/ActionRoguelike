using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRegisterRoomChild : MonoBehaviour, Initializable
{
    [SerializeField] RoomChild RoomChild;

    public void Init()
    {
        Room r = TraverseManager.GetRoom((int)transform.position.x, (int)transform.position.y);
        r.AddRoomChild(RoomChild);
    }
}
