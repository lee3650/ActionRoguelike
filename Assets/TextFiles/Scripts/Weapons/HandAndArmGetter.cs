using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAndArmGetter : MonoBehaviour
{
    public Transform Hand;
    public Transform Arm;

    public void SetHandAndArm(Transform hand, Transform arm)
    {
        Arm = arm;
        Hand = hand; 
    }
}
