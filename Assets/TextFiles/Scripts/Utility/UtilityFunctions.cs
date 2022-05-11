using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions : MonoBehaviour
{
    public static float AnimateArc(float start, float dist, int dir, float t)
    {
        return UtilityFunctions.LerpAngleDirection(start, dir * dist + start, t, dir);
    }

    public static Vector2 GetDirectionFromRotation(float dir)
    {
        return new Vector2(Mathf.Cos(dir * Mathf.Deg2Rad), Mathf.Sin(dir * Mathf.Deg2Rad));
    }
    
    public static Vector2Int RoundVectorToInt(Vector2 vector)
    {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    public static int ClosestVector(Vector2 dir, Vector2Int[] Directions, float DirectionalThreshold)
    {
        for (int i = 0; i < Directions.Length; i++)
        {
            if (dir == Directions[i] || Vector2.Dot(Directions[i], dir) >= DirectionalThreshold)
            {
                return i;
            }
        }
        return 0; //?
    }

    public static float GetRotationFromDirection(Vector2 dir)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
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

    public static Color LerpAlpha(Color input, float start, float end, float t)
    {
        return new Color(input.r, input.g, input.b, Mathf.Lerp(start, end, t));
    }
}
