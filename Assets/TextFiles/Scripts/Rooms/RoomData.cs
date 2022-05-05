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

    public static RoomData Copy(RoomData og)
    {
        RoomData result = new RoomData();
        result.Room = og.Room;
        result.DestructiveEnv = og.DestructiveEnv;
        result.AdditiveEnv = og.AdditiveEnv;
        result.Wave = og.Wave;
        result.Offset = og.Offset;

        result.Parent = og.Parent;

        return result; 
    }

    /// <summary>
    /// Used to help with room placement
    /// </summary>
    public RoomData Parent
    {
        get;
        set;
    }

    public int XSizeWorld
    {
        get
        {
            return XSize * MapDrawer.TileSize; 
        }
    }

    public int YSizeWorld
    {
        get
        {
            return YSize * MapDrawer.TileSize;
        }
    }

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