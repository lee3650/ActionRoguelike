using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleOverTime : MonoBehaviour, Initializable
{
    [SerializeField] Transform Parent; 
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float wiggleAmount, wiggleFreq;
    float timer = 0f;

    float angle = 0f;

    private List<float> dirs = new List<float>();

    private int curRot, nextRot;

    public void Init()
    {
        dirs.Add(0);
        dirs.Add(wiggleAmount);
        dirs.Add(0);
        dirs.Add(-wiggleAmount);

        curRot = 0;
        nextRot = 1;
    }
    
    private int WrapInt(int original)
    {
        original++;
        if (original >= dirs.Count)
        {
            original = 0;
        }
        return original; 
    }

    private void FixedUpdate()
    {
        if (playerRb.velocity.magnitude > 1f)
        {
            timer += Time.fixedDeltaTime;
            angle = Mathf.LerpAngle(dirs[curRot], dirs[nextRot], timer * wiggleFreq);
            if (timer * wiggleFreq >= 1)
            {
                timer = 0f;
                curRot = WrapInt(curRot);
                nextRot = WrapInt(nextRot);
            }
            //adjustment = wiggleAmount * Mathf.Sin(2 * Mathf.PI * timer * wiggleFreq);
        } else
        {
            timer = 0f; 
            angle = Mathf.LerpAngle(Parent.localPosition.z, 0, 0.2f);
            curRot = 0;
            nextRot = 1;
        }

        Parent.localEulerAngles = new Vector3(Parent.localEulerAngles.x, Parent.localEulerAngles.y, angle);
    }

    public void ResetWiggle()
    {
        angle = 0f; 
        curRot = 0;
        nextRot = 1;
        timer = 0f; 
    }
}
