using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLineDist : MonoBehaviour
{
    [SerializeField] LineRenderer lr;
    [SerializeField] LineOfSight LineOfSight;
    [SerializeField] RoomChild RoomChild;

    const string layer = "Trap";

    private float furthestDist; 

    private void FixedUpdate()
    {
        if (RoomChild.RoomActive())
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, LineOfSight.VisiblePoint);
        } else
        {
            lr.enabled = false;
        }
    }
}
