using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMinimap : MonoBehaviour
{
    [SerializeField] PlayerGetter PlayerGetter;
    [SerializeField] Camera Camera;
    private PlayerRoomSetter PlayerRoom;
    private Transform player; 

    private void Awake()
    {
        PlayerGetter.PlayerReady += PlayerReady;
    }

    private void PlayerReady(Transform obj)
    {
        PlayerRoom = obj.GetComponent<PlayerRoomSetter>();
        player = obj;
    }

    void FixedUpdate()
    {
        if (PlayerRoom.RoomClear)
        {
            Camera.enabled = true;
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        } else
        {
            Camera.enabled = false;
        }
    }
}
