using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGrid : MonoBehaviour
{
    [SerializeField] List<Transform> GridItems;

    public (Vector3, float) GetNearestGridItem(Vector3 current)
    {
        float min = 100000f;
        Vector3 closest = new Vector3(); 
        foreach (Transform t in GridItems)
        {
            float dist = Vector3.Distance(t.position, current);
            if (dist < min)
            {
                min = dist;
                closest = t.position;
            }
        }

        return (closest, min);
    }
}
