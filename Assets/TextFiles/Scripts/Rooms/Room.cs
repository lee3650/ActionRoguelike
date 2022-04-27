using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager[] Enemies;
    [SerializeField] Doormat[] Doormats;
    [SerializeField] Door[] Doors;
    [SerializeField] RoomChild[] Children; 

    private PlayerRoomSetter setter;
    private bool entered = false;
    private bool roomActive = false;

    public event System.Action RoomEntered = delegate { };

    public void LateInit()
    {
        entered = false;
        roomActive = false; 

        foreach (Doormat d in Doormats)
        {
            d.SetMyRoom(this);
        }

        foreach (HealthManager hm in Enemies)
        {
            hm.OnDeath += OnDeath;
        }

        foreach (RoomChild c in Children)
        {
            c.Parent = this; 
        }
    }

    public bool RoomActive
    {
        get
        {
            return roomActive; 
        }
    }

    private void OnDeath()
    {
        if (!AnyEnemiesLive())
        {
            ModifyAllDoors(true);
            setter.RoomClear = true;
            roomActive = false; 
        }
    }

    private void ModifyAllDoors(bool open)
    {
        foreach (Door d in Doors)
        {
            if (open)
            {
                d.Open();
            } else
            {
                d.Close();
            }
        }
    }

    public void TriggeredDoormat(PlayerRoomSetter player)
    {
        print("got triggered!");

        setter = player; 

        if (AnyEnemiesLive() && !entered)
        {
            print("doing stuff!");
            entered = true;
            setter.RoomClear = false;
            roomActive = true;
            RoomEntered();

            foreach (Doormat d in Doormats)
            {
                Destroy(d.gameObject);
            }

            Doormats = new Doormat[0];

            foreach (HealthManager hm in Enemies)
            {
                OrderedInit.PerformInitialization(hm.transform);
            }

            ModifyAllDoors(false);
        }
    }

    private bool AnyEnemiesLive()
    {
        foreach (HealthManager hm in Enemies)
        {
            if (hm.IsAlive())
            {
                return true; 
            }
        }
        return false; 
    }
}
