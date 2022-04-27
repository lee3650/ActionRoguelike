using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour, LateInitializable
{
    [SerializeField] string[] blacklistLayers = { "Trap", "TrapObject" };

    private float unbrokenDistSqr;
    
    private int mask; 

    public void LateInit()
    {
        mask = ~LayerMask.GetMask(blacklistLayers);
        unbrokenDistSqr = ((Vector2)transform.position - GetHit().point).sqrMagnitude;
    }

    private RaycastHit2D GetHit()
    {
        return Physics2D.Raycast(transform.position, transform.up, 100f, mask);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = GetHit();
        VisiblePoint = hit.point;

        float sqrDist = ((Vector2)transform.position - VisiblePoint).sqrMagnitude;
        if (sqrDist < unbrokenDistSqr - 0.05f)
        {
            Broken = true; 
        }
        else
        {
            Broken = false; 
        }
    }

    public Vector2 VisiblePoint
    {
        get;
        private set; 
    }

    public bool Broken
    {
        get;
        private set;
    }
}
