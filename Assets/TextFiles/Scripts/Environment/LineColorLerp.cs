using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColorLerp : MonoBehaviour
{
    [SerializeField] LineRenderer lr; 

    public void LerpLineOpacity(float t)
    {
        lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, t);
        lr.endColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, t);
    }
}
