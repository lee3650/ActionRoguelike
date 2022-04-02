using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTalentManager : MonoBehaviour, Initializable
{
    public const int MaxActiveTalents = 6;

    [SerializeField] State[] ActiveTalents = new State[MaxActiveTalents];

    private int highestTalent = 0;

    public void Init()
    {
        highestTalent = 0;
        for (int i = MaxActiveTalents - 1; i >= 1; i--)
        {
            if (ActiveTalents[i - 1] != null)
            {
                highestTalent = i;
            }
        }
    }

    public int GetTalentIndex(State talent) 
    {
        int result = -1; 
        for (int i = 0; i < highestTalent; i++)
        {
            if (ActiveTalents[i] == talent)
            {
                result = i;
                break;
            }
        }
        return result; 
    }

    public void AddTalent(State newTalent)
    {
        if (highestTalent < MaxActiveTalents)
        {
            ActiveTalents[highestTalent] = newTalent;
            highestTalent++;
        }
    }

    public void RemoveTalent(State talent)
    {
        int ind = GetTalentIndex(talent);
        if (ind >= 0)
        {
            //everything right of ind we shift left by 1
            for (int i = ind; i < highestTalent - 1; i++)
            {
                ActiveTalents[i] = ActiveTalents[i + 1];
            }
            highestTalent--;
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

    public bool IsTalentAllowed(int index)
    {
        if (IsActiveTalentValid(index))
        {
            return ((Talent)ActiveTalents[index]).CanUseTalent();
        }
        return false; 
    }

    public State GetActiveTalent(int index)
    {
        return ActiveTalents[index];
    }

    /// <summary>
    /// Test method. Not for production use. 
    /// </summary>
    public void SetTalentSlot(int i, State talent)
    {
        ActiveTalents[i] = talent;
    }
}
