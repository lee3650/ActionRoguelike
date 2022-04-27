using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChild : MonoBehaviour
{
    public event System.Action RoomEntered = delegate { };

    private Room parent; 

    public Room Parent
    {
        private get
        {
            return parent; 
        }
        set
        {
            parent = value;
            parent.RoomEntered += roomEntered;
        }
    }

    private void roomEntered()
    {
        RoomEntered();
    }

    public bool RoomActive()
    {
        return Parent.RoomActive; 
    }
}
