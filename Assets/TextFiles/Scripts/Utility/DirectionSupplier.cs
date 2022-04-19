using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSupplier : MonoBehaviour
{
    public Vector2 GetDir()
    {
        return transform.right.normalized; 
    }
}
