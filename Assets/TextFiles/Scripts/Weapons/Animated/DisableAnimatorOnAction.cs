using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimatorOnAction : MonoBehaviour, LateInitializable
{
    [SerializeField] GenericWeapon Weapon;
    [SerializeField] Animator Animator;
    [SerializeField] List<string> EnabledActions; 
    [SerializeField] List<string> DisabledActions;

    public void LateInit()
    {
        Weapon.OnStartAction += OnStartAction;
    }

    private void OnStartAction(string obj)
    {
        if (EnabledActions.Contains(obj))
        {
            Animator.enabled = true; 
        }
        else if (DisabledActions.Contains(obj))
        {
            Animator.enabled = false;
        }
    }
}
