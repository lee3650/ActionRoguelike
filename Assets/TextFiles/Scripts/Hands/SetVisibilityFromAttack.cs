using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisibilityFromAttack : MonoBehaviour
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] bool attackingVisibility;

    private void FixedUpdate()
    {
        if (!WeaponManager.ActionFinished())
        {
            //I'm not crazy about this... now if we want to hide the hands it's trickier. 
            sr.enabled = attackingVisibility;
        } else
        {
            sr.enabled = !attackingVisibility;
        }
    }
}
