using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAnimator : MonoBehaviour
{
    public static void AnimateSwing(HandAndArmGetter handAndArm, float startArm, float startWrist, float wristDist, float armDist, float length, float timer, int armDir, int wristDir)
    {
        float t = timer / length;

        handAndArm.AnimateArcArm(startArm, armDist, t, armDir);
        handAndArm.AnimateArcHand(startWrist, wristDist, t, wristDir);
    }

    public static void AnimateRecovery(HandAndArmGetter handAndArm, float startArm, float startWrist, float end, float length, float timer, int dir, int wristDir)
    {
        float t = timer / length;

        handAndArm.SetArmRotation(UtilityFunctions.LerpAngleDirection(startArm, end, t, dir));
        handAndArm.SetHandRotation(UtilityFunctions.LerpAngleDirection(startWrist, end, t, wristDir));
    }

    public static float GetStart(bool reversed)
    {
        if (reversed)
        {
            return 180;
        }
        return 0f;
    }

    public static int GetStartDirection(bool reversed)
    {
        if (reversed)
        {
            return 1;
        }
        return -1; 
    }
}
