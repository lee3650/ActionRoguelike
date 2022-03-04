using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] Transform WeaponCenterParent;
    [SerializeField] Weapon CurrentWeapon; 

    private bool faceInput = true; 

    private void FixedUpdate()
    {
        if (faceInput)
        {
            FacePlayerInput.FaceInput();
        }
    }

    public void StartAttack()
    {
        CurrentWeapon.StartAttack();
    }

    public bool AttackFinished()
    {
        return CurrentWeapon.AttackFinished();
    }

    public void PauseFaceInput()
    {
        faceInput = false;
    }

    public void PlayFaceInput()
    {
        faceInput = true;
    }
}
