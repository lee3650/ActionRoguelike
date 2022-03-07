using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    public const int MaxActiveTalents = 5;

    [SerializeField] State[] ActiveTalents = new State[MaxActiveTalents];

    private int highestTalent = 0;

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
