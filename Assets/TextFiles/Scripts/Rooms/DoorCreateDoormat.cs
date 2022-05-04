using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCreateDoormat : MonoBehaviour, Initializable
{
    [SerializeField] Doormat DoormatPrefab; 
    public void Init()
    {
        Vector2Int[] options = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1)
        };

        Room myRoom = TraverseManager.GetRoom((int)transform.position.x, (int)transform.position.y);

        for (int i = 0; i < options.Length; i++)
        {
            Vector2Int current = new Vector2Int((int)transform.position.x, (int)transform.position.y);

            Vector2Int plus = current + (options[i] * MapDrawer.TileSize);
            Vector2Int minus = current - (options[i] * MapDrawer.TileSize);

            Room plusRoom = TraverseManager.GetRoom(plus.x, plus.y);

            Room minusRoom = TraverseManager.GetRoom(minus.x, minus.y);

            if (plusRoom == myRoom && minusRoom != myRoom)
            {
                Instantiate(DoormatPrefab, new Vector3(plus.x, plus.y, 0) + new Vector3(options[i].x, options[i].y, 0), Quaternion.identity);
                return;
            }
        }
    }
}
