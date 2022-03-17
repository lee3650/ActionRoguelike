using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager[] Enemies;
    [SerializeField] Doormat[] Doormats;
    [SerializeField] Door[] Doors;

    private PlayerRoomSetter setter;
    private bool entered = false;

    public void LateInit()
    {
        entered = false;

        foreach (Doormat d in Doormats)
        {
            d.SetMyRoom(this);
        }

        foreach (HealthManager hm in Enemies)
        {
            hm.OnDeath += OnDeath;
        }
    }

    private void OnDeath()
    {
        if (!AnyEnemiesLive())
        {
            ModifyAllDoors(true);
            setter.RoomClear = true; 
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
