using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimator : MonoBehaviour
{
    [SerializeField] Animator Animator; 
    [SerializeField] AnimationClip[] RunAnimations;
    [SerializeField] AnimationClip[] IdleAnimations;
    [SerializeField] float DirectionalThreshold = 0.5f; 
    [SerializeField] float IdleThreshold = 0.5f;

    [Tooltip("Directions to order animations")]
    [SerializeField]
    Vector2Int[] Directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1),
    };

    private int IndexOf(Vector2Int dir) 
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

    public void AnimateRunDirection(float dir, float velocity)
    {
        Vector2 direction = UtilityFunctions.GetDirectionFromRotation(dir);
        Vector2Int round = UtilityFunctions.RoundVectorToInt(direction);

        AnimationClip[] clips = RunAnimations; 

        if (velocity < IdleThreshold)
        {
            clips = IdleAnimations;
        } 

        Animator.Play(clips[IndexOf(round)].name);
    }
}
