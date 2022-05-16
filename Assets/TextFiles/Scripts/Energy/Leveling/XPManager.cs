using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XPManager : MonoBehaviour, Initializable
{
    //public event Action LeveledUp = delegate { };
    //public event Action XPChanged = delegate { };
    public event Action ModuleComplete = delegate { };
    public event Action ProgressChanged = delegate { };

    private static XPManager instance;

    private TalentPolicy CurrentPolicy;

    public void Init()
    {
        instance = this;
        /*
        curLevel = startingLevel;
        curXP = startXP;
        xpReq = startXPReq;
        LeveledUp = delegate { };
        XPChanged = delegate { };
         */
        ModuleComplete = delegate { };
        ProgressChanged = delegate { };
    }

    public string GetCurrentPolicyTitle()
    {
        if (CurrentPolicy == null)
        {
            return "";
        }
        return CurrentPolicy.Title;
    }

    public TalentPolicy GetCurrentPolicy()
    {
        return CurrentPolicy; 
    }

    public bool HasCurrentPolicy()
    {
        return CurrentPolicy != null;
    }

    public bool HasPolicyInProgress()
    {
        return HasCurrentPolicy() && GetXPPercentage() < 1; 
    }

    public void ProgressPolicy(int amt)
    {
        CurrentPolicy.Progress += amt;

        ProgressChanged();

        if (CurrentPolicy.Progress >= CurrentPolicy.GetCost())
        {
            ModuleComplete();
        }
    }

    public void SetPolicy(TalentPolicy policy)
    {
        CurrentPolicy = policy;
        ProgressChanged();
    }

    public float GetXPPercentage()
    {
        if (CurrentPolicy == null || CurrentPolicy.GetCost() == 0)
        {
            return 0f; 
        }
        return ((float)CurrentPolicy.Progress / CurrentPolicy.GetCost());
    }
    
    /*
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
     */

    public static void AddXP(float amt)
    {
        instance.ProgressPolicy((int)amt);
        //instance.XPGained(amt);
    }

    public static void SetCurrentPolicy(TalentPolicy p)
    {
        instance.SetPolicy(p);
    }
}