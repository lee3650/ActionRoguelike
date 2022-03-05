using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, SecondInitializable
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] Transform WeaponCenterParent;
    [SerializeField] Weapon CurrentWeapon;
    [SerializeField] PlayerWielder PlayerWielder;
    
    private bool faceInput = true; 

    public void SecondInit()
    {
        CurrentWeapon.SetWielder(PlayerWielder);
    }

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
