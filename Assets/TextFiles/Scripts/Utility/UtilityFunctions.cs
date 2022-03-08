using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions : MonoBehaviour
{
    public static float AnimateArc(float start, float dist, int dir, float t)
    {
        return UtilityFunctions.LerpAngleDirection(start, dir * dist + start, t, dir);
    }

    /// <summary>
    /// Direction = -1, 1, or 0. 0 is equivalent to Mathf.LerpAngle. 
    /// </summary>
    public static float LerpAngleDirection(float start, float end, float t, int direction)
    {
        if (direction != 1 && direction != -1 && direction != 0)
        {
            throw new System.Exception("invalid direction!");
        }

        start %= 360;
        end %= 360;

        if (direction == 0)
        {
            return Mathf.LerpAngle(start, end, t);
        }

        t = Mathf.Clamp(t, 0f, 1f);

        //if it's positive, we want end > start
        if (direction > 0)
        {
            if (end < start)
            {
                end += 360;
            }
        }
        else
        {
            //if it's negative, we want end < start
            if (end > start)
            {
                end -= 360;
            }
        }

        float delta = end - start;

        return (t * delta) + start;
    }
}
