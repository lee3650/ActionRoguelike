using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public Sprite Room;
    public Sprite DestructiveEnv;
    public Sprite AdditiveEnv;
    public Sprite Wave;
    public Vector2Int Offset; 

    public int XSize
    {
        get
        {
            return Mathf.RoundToInt(Room.rect.width);
        }
    }

    public int YSize
    {
        get
        {
            return Mathf.RoundToInt(Room.rect.height);
        }
    }
}