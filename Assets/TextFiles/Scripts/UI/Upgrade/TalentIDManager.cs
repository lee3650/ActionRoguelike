using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentIDManager : MonoBehaviour
{
    [SerializeField] private List<TalentPolicy> AllTalents;

    private void Awake()
    {
        List<int> used = new List<int>();
        foreach (TalentPolicy t in AllTalents)
        {
            if (used.Contains(t.ID))
            {
                throw new System.Exception("Duplicate ID: " + t.ID + ", " + t.Title);
            }
            used.Add(t.ID);
        }
    }

    public TalentPolicy GetPrefab(int id)
    {
        foreach (TalentPolicy tp in AllTalents)
        {
            if (tp.ID == id)
            {
                return tp;
            }
        }
        return null;
    }
}
