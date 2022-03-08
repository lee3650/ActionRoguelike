using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour, Initializable
{
    public const int MaxActiveTalents = 5;

    [SerializeField] State[] ActiveTalents = new State[MaxActiveTalents];

    private int highestTalent = 0;

    public void Init()
    {
        highestTalent = 0;
        for (int i = MaxActiveTalents; i >= 1; i--)
        {
            if (ActiveTalents[i - 1] != null)
            {
                highestTalent = i;
                break;  
            }
        }
    }

    public void AddTalent(State newTalent)
    {
        if (highestTalent < MaxActiveTalents)
        {
            ActiveTalents[highestTalent] = newTalent;
            highestTalent++;
        }
    }

    public int GetNumberOfActiveTalents()
    {
        return highestTalent;
    }

    public bool IsActiveTalentValid(int index)
    {
        return index >= 0 && index < highestTalent; 
    }

    public State GetActiveTalent(int index)
    {
        return ActiveTalents[index];
    }
}
