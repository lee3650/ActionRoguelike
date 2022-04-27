using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class XPManager : MonoBehaviour, Initializable
{
    public event Action LeveledUp = delegate { };
    public event Action XPChanged = delegate { };

    private static XPManager instance;

    [SerializeField] int startingLevel = 1;
    [SerializeField] float startXP = 0f;
    [SerializeField] float startXPReq = 4.5f;
    [SerializeField] float XPSlope = 1.5f;
    [SerializeField] float XPPower = 1f;
    [Space(20)]
    [SerializeField] int curLevel;
    [SerializeField] float curXP;
    [SerializeField] float xpReq; 

    public void Init()
    {
        instance = this;
        curLevel = startingLevel;
        curXP = startXP;
        xpReq = startXPReq;
        LeveledUp = delegate { };
        XPChanged = delegate { };
    }

    public float GetXPPercentage()
    {
        return curXP / xpReq; 
    }

    public int GetCurLevel()
    {
        return curLevel; 
    }

    private void XPGained(float amt)
    {
        curXP += amt;
        
        XPChanged();
        
        if (curXP >= xpReq)
        {
            float leftover = curXP - xpReq;
            curXP = 0;
            AdjustXPRequirements();
            curLevel++;
            
            LeveledUp();
           
            XPGained(leftover);
        }
    }

    private void AdjustXPRequirements()
    {
        xpReq = startXPReq + (XPSlope * Mathf.Pow(curLevel, XPPower)); 
    }

    public static void AddXP(float amt)
    {
        instance.XPGained(amt);
    }
}