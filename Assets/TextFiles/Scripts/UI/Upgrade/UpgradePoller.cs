using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpgradePoller : MonoBehaviour, Initializable
{
    [SerializeField] List<MousePoll> MousePolls;
    [SerializeField] TalentPolicy EmptyPolicy;
    [SerializeField] TalentDisplayer TalentDisplayer;

    private TalentPolicy SelectedPolicy;
    private TalentPolicy lastShown;

    public void Init()
    {
        SelectedPolicy = EmptyPolicy;
        lastShown = EmptyPolicy;
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

    public void SelectPolicy(TalentPolicy policy)
    {
        if (policy == null)
        {
            SelectedPolicy = EmptyPolicy; 
        } 
        else
        {
            SelectedPolicy = policy;
        }
    }

    public void RefreshPolicy()
    {
        lastShown = null;
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
                if (lastShown != tg.Policy) 
                {
                    lastShown = tg.Policy;
                    DisplayTalent(tg.Policy);
                } 
                break;
            }
        }

        if (!found)
        {
            if (lastShown != SelectedPolicy)
            {
                lastShown = SelectedPolicy;
                DisplaySelectedTalent();
            }
        }
    }

    private void DisplaySelectedTalent()
    {
        TalentDisplayer.DisplaySelectedTalent(SelectedPolicy);
    }

    private void DisplayTalent(TalentPolicy Policy)
    {
        TalentDisplayer.DisplayTalent(Policy);
    }
}
