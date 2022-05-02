using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTraversablePoint : MonoBehaviour, Initializable
{
    [SerializeField] float adjustment;
    
    public void Init()
    {
        Vector2Int[] dirs = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
        };

        Vector2Int targetDir = dirs[0];
        
        for (int i = 0; i < dirs.Length; i++)
        {
            Vector2Int testPos = new Vector2Int((int)transform.position.x, (int)transform.position.y) + (MapDrawer.TileSize * dirs[i]);
            if (TraverseManager.IsPointTraversable(testPos.x, testPos.y))
            {
                targetDir = dirs[i];
                break; 
            }
        }

        transform.localEulerAngles = new Vector3(0, 0, UtilityFunctions.GetRotationFromDirection(targetDir) + adjustment);
    }
}
