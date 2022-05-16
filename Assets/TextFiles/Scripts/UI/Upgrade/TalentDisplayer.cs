using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentDisplayer : MonoBehaviour
{
    public abstract void DisplayTalent(TalentPolicy tp);

    public abstract void DisplaySelectedTalent(TalentPolicy tp);

    protected string GetCostText(TalentPolicy policy)
    {
        if (policy.GetCost() < 0)
        {
            return "";
        }
        else
        {
            if (policy.Progress == 0)
            {
                return "Cost: " + policy.GetCost() + " scrap";
            }
            else
            {
                return string.Format("{0} / {1} scrap", policy.Progress, policy.GetCost());
            }
        }
    }
}
