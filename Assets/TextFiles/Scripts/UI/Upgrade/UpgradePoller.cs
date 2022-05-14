using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradePoller : MonoBehaviour, Initializable
{
    [SerializeField] List<MousePoll> MousePolls;
    [SerializeField] TextMeshProUGUI UpgradeName;
    [SerializeField] TextMeshProUGUI UpgradeDescription;
    [SerializeField] TextMeshProUGUI UpgradeCost;
    [SerializeField] TalentPolicy EmptyPolicy;

    private TalentPolicy DefaultPolicy;

    public void Init()
    {
        DefaultPolicy = EmptyPolicy; 
    }

    public void ResetPolls()
    {
        foreach (MousePoll p in MousePolls)
        {
            Destroy(p.gameObject);
        }

        MousePolls = new List<MousePoll>();
    }

    public void AddPoll(MousePoll poll)
    {
        MousePolls.Add(poll);
    }

    public void SetDefaultPolicy(TalentPolicy policy)
    {
        if (policy == null)
        {
            DefaultPolicy = EmptyPolicy; 
        } 
        else
        {
            DefaultPolicy = policy;
        }
    }

    private void Update()
    {
        bool found = false;

        for (int i = MousePolls.Count - 1; i >= 0; i--) 
        {
            MousePoll mp = MousePolls[i];
            
            if (mp == null)
            {
                MousePolls.RemoveAt(i);
                continue; 
            }

            if (mp.MousedOver)
            {
                TalentGetter tg = mp.GetComponent<TalentGetter>();
                found = true;
                DisplayTalent(tg.Policy);

                break;
            }
        }

        if (!found)
        {
            DisplayTalent(DefaultPolicy);
        }
    }

    private void DisplayTalent(TalentPolicy Policy)
    {
        UpgradeName.text = Policy.Title;
        UpgradeDescription.text = Policy.Description;
        UpgradeCost.text = GetCostText(Policy);

        foreach (TalentPolicy tp in Policy.GetAppliedUpgrades())
        {
            UpgradeDescription.text += string.Format("\n\nUpgrade: {0}\nCost: {1} scrap", tp.Description, tp.GetCost());
        }
    }

    private string GetCostText(TalentPolicy policy)
    {
        if (policy.GetCost() == 0)
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
