using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAndArmGetter : MonoBehaviour, Initializable
{
    [SerializeField] private Transform Hand;
    [SerializeField] private Transform Arm;
    [SerializeField] private Transform LeftHand;

    private Vector2 handStartPos;

    public void HideHands()
    {
        Hand.gameObject.SetActive(false);
        if (LeftHand != null)
        {
            LeftHand.gameObject.SetActive(false);
        }
    }
    public void ShowHands()
    {
        Hand.gameObject.SetActive(true);
        if (LeftHand != null)
        {
            LeftHand.gameObject.SetActive(true);
        }
    }

    public void SetHandRotation(float rot)
    {
        Hand.localEulerAngles = new Vector3(0f, 0f, rot);
    }

    public void Init()
    {
        handStartPos = Hand.localPosition; 
    }

    public void AnimateHandOut(float dist, float t)
    {
        Hand.localPosition = Vector2.Lerp(handStartPos, new Vector2(0, handStartPos.y - dist), t);
    }

    public void AnimateHandReset(float dist, float t)
    {
        Hand.localPosition = Vector2.Lerp(new Vector2(0, handStartPos.y - dist), handStartPos, t);
    }

    public void SetHandPosition(Vector2 pos)
    {
        Hand.position = pos; 
    }

    public void ResetHandPosition()
    {
        Hand.localPosition = handStartPos; 
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

    public void ResetPositions(bool reversed)
    {
        float armRot = 0f;
        float handRot = 0f;

        if (reversed)
        {
            armRot = 180f;
            handRot = -180f; 
        }

        SetArmRotation(armRot);
        SetHandRotation(handRot);
        ResetHandPosition();
    }

    public void SetHandAndArm(Transform hand, Transform arm)
    {
        Arm = arm;
        Hand = hand; 
    }
}
