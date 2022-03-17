using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doormat : MonoBehaviour
{
    private Room room; 

    public void SetMyRoom(Room room)
    {
        this.room = room; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerRoomSetter>(out PlayerRoomSetter p))
        {
            //technically I need to move player summons through as well, right?
            //Could be worth looking into 
            print("player collided!");
            room.TriggeredDoormat(p);
        }
    }
}
