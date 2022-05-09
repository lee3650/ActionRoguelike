using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnInit : MonoBehaviour, Initializable
{
    [SerializeField] Door Door;

    public void Init()
    {
        //So, if there aren't 2 or more, um, traversable tiles, then we should lock the door

        Vector2Int[] dirs = new Vector2Int[] 
        {
            new Vector2Int(0, 1),       
            new Vector2Int(1, 0),       
            new Vector2Int(0, -1),       
            new Vector2Int(-1, 0),
        };

        Vector2Int cur = UtilityFunctions.RoundVectorToInt(transform.position);

        for (int i = 0; i < dirs.Length; i++)
        {
            if (TraverseManager.GetRoom(cur + (dirs[i] * MapDrawer.TileSize)) == null)
            {
                Door.LockDoor();
                break;
            }
        }
    }
}
