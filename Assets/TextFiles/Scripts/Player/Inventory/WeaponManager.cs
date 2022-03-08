using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] PlayerWielder PlayerWielder;
    
    private Weapon CurrentWeapon;
    private bool faceInput = true; 

    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon; 
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        CurrentWeapon?.Deselect();
        newWeapon.SetWielder(PlayerWielder);
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


    public void StartAction(string action)
    {
        CurrentWeapon.StartAction(action);

        switch (action)
        {
            case "throw":
                CurrentWeapon = null;
                break; 
        }
    }

    public bool ActionFinished()
    {
        return CurrentWeapon.ActionFinished();
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
