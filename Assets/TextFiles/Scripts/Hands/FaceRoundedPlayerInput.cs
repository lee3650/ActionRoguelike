using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceRoundedPlayerInput : MonoBehaviour
{
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] ReversedTracker ReversedTracker;

    Vector2Int[] Directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1),
    };

    private bool previousReversed = false;

    private void FixedUpdate()
    {
        float dir = PlayerInput.GetDirectionToFace();
        Vector2 unround = UtilityFunctions.GetDirectionFromRotation(dir);
        Vector2Int round = UtilityFunctions.RoundVectorToInt(unround);

        bool snapPosition = false; 

        if (previousReversed != ReversedTracker.Reversed)
        {
            //it changed this frame 
            snapPosition = true; 
        }

        if (ReversedTracker.Reversed)
        {
            round *= -1; 
        }

        Vector2 snap = Directions[UtilityFunctions.ClosestVector(round, Directions, 0.5f)];

        float true_dir = UtilityFunctions.GetRotationFromDirection(snap);
        if (!snapPosition)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.LerpAngle(transform.localEulerAngles.z, true_dir, 0.25f));
        } else
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, true_dir);
        }

        previousReversed = ReversedTracker.Reversed; 
    }
}
