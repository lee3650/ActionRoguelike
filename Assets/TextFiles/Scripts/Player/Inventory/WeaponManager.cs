using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform Arm;
    [SerializeField] Transform Hand;
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] Weapon CurrentWeapon;
    [SerializeField] PlayerWielder PlayerWielder;
    
    private bool faceInput = true; 

    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon; 
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        CurrentWeapon?.Deselect();
        newWeapon.SetWielder(PlayerWielder, Hand, Arm);
        newWeapon.Select();
        CurrentWeapon = newWeapon;
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
