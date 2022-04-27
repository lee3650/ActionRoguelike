using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFromAction : MonoBehaviour, LateInitializable
{
    [SerializeField] GenericWeapon GenericWeapon;
    [SerializeField] string[] Actions = new string[] { ActionStrings.AttackAction, ActionStrings.ThrowAction };
    [SerializeField] bool[] EnabledState = new bool[2];
    [SerializeField] Component[] ApplicableComponents;

    private List<Enableable> enableables; 

    public void LateInit()
    {
        GenericWeapon.OnStartAction += OnStartAction;
        enableables = new List<Enableable>();
        foreach (Component c in ApplicableComponents)
        {
            enableables.Add(c as Enableable);
        }
    }

    private void OnStartAction(string obj)
    {
        bool found = false;
        bool active = false;
        for (int i = 0; i < Actions.Length; i++)
        {
            if (Actions[i].Equals(obj))
            {
                found = true;
                active = EnabledState[i];
                break; 
            } 
        }

        if (found)
        {
            foreach (Enableable e in enableables)
            {
                e.SetEnabled(active);
            }
        }
    }
}
