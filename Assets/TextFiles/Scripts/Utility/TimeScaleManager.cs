using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    private bool inUntimedFreeze = false; 

    public void BeginUntimedFreeze()
    {
        StopAllCoroutines();
        inUntimedFreeze = true;
        Time.timeScale = 0f; 
    }

    IEnumerator TimedFreeze(float len)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(len);
        Time.timeScale = 1f; 
    }

    public void BeginFreeze(float len)
    {
        if (!inUntimedFreeze)
        {
            StopAllCoroutines();
            StartCoroutine(TimedFreeze(len));
        }
    }

    public void EndUntimedFreeze()
    {
        inUntimedFreeze = false;
        Time.timeScale = 1f; 
    }
}
