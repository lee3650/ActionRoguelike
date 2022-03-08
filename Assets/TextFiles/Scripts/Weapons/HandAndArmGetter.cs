using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAndArmGetter : MonoBehaviour
{
    [SerializeField] private Transform Hand;
    [SerializeField] private Transform Arm;

    public void SetHandRotation(float rot)
    {
        Hand.localEulerAngles = new Vector3(0f, 0f, rot);
    }

    public void SetArmRotation(float rot)
    {
        Arm.localEulerAngles = new Vector3(0f, 0f, rot);
    }

    public void AnimateArcHand(float start, float distance, float t, int dir)
    {
        SetHandRotation(UtilityFunctions.AnimateArc(start, distance, dir, t));
    }

    public void AnimateArcArm(float start, float distance, float t, int dir)
    {
        SetArmRotation(UtilityFunctions.AnimateArc(start, distance, dir, t));
    }

    public float GetArmRotation()
    {
        return Arm.localEulerAngles.z;
    }

    public float GetHandRotation()
    {
        return Hand.localEulerAngles.z;
    }

    public void SetHandAndArm(Transform hand, Transform arm)
    {
        Arm = arm;
        Hand = hand; 
    }
}
