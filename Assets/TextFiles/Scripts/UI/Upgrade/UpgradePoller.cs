using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradePoller : MonoBehaviour, LateInitializable
{
    [SerializeField] List<MousePoll> MousePolls;
    [SerializeField] TextMeshProUGUI UpgradeName;
    [SerializeField] TextMeshProUGUI UpgradeDescription;
    [SerializeField] TextMeshProUGUI UpgradeCost;
    [SerializeField] TalentPolicy EmptyPolicy;

    private TalentPolicy DefaultPolicy;

    public void LateInit()
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

    private void FixedUpdate()
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
                UpgradeName.text = tg.Policy.Title;
                UpgradeDescription.text = tg.Policy.Description;
                UpgradeCost.text = "Cost: 7 scrap";
                break;
            }
        }

        if (!found)
        {
            UpgradeName.text = DefaultPolicy.Title;
            UpgradeDescription.text = DefaultPolicy.Description;
            UpgradeCost.text = "";
        }
    }
}
