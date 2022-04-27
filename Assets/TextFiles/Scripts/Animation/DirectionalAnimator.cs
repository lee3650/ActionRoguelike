using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimator : MonoBehaviour
{
    [SerializeField] Animator Animator; 
    [SerializeField] AnimationClip[] RunAnimations;
    [SerializeField] AnimationClip[] IdleAnimations;
    [SerializeField] AnimationClip[] RollAnimations;
    [SerializeField] float IdleThreshold = 0.5f;
    const float DirectionalThreshold = 0.5f;

    [Tooltip("Directions to order animations")]
    [SerializeField]
    Vector2Int[] Directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1),
    };

    public void AnimateRoll(Vector2 dir)
    {
        Vector2Int round = UtilityFunctions.RoundVectorToInt(dir);

        AnimationClip[] clips = RollAnimations;

        Animator.Play(clips[UtilityFunctions.ClosestVector(round, Directions, DirectionalThreshold)].name);
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

        Animator.Play(clips[UtilityFunctions.ClosestVector(round, Directions, DirectionalThreshold)].name);
    }
}
