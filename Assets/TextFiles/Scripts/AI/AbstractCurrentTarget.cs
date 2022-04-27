using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCurrentTarget : MonoBehaviour
{
    public Vector2 GetTargetPosition()
    {
        return ClosestTarget.GetMyPosition();
    }

    public Targetable ClosestTarget
    {
        get;
        protected set;
    }

    public bool HasTarget
    {
        get
        {
            return ClosestTarget != null && ClosestTarget.IsAlive();
        }
    }
}
