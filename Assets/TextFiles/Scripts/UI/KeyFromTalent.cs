using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFromTalent : MonoBehaviour
{
    [SerializeField] ActiveTalentManager ActiveTalentManager;
    [SerializeField] PlayerInput PlayerInput;

    public string GetKeyForActiveTalent(State t)
    {
        int i = ActiveTalentManager.GetTalentIndex(t);
        if (i >= 0)
        {
            string result = PlayerInput.GetTalentKey(i);
            return result; 
        }
        return "";
    }

    public string GetNextAvailableKey()
    {
        string result = PlayerInput.GetTalentKey(ActiveTalentManager.GetNumberOfActiveTalents());
        if (ActiveTalentManager.GetNumberOfActiveTalents() == ActiveTalentManager.MaxActiveTalents)
        {
            result += "\nThis will overwrite the talent currently bound to " + result;
        }
        return result; 
    }
}
