using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLineDist : MonoBehaviour
{
    [SerializeField] LineRenderer lr;
    [SerializeField] LineOfSight LineOfSight;
    const string layer = "Trap";

    private float furthestDist; 

    private void FixedUpdate()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, LineOfSight.VisiblePoint);
    }
}
