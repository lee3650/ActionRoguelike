using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : AbstractWeaponManager
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    
    private bool faceInput = true; 

    public Weapon GetCurrentWeapon()
    {
        return CurrentWeapon; 
    }

    private void FixedUpdate()
    {
        if (faceInput)
        {
            FacePlayerInput.FaceInput();
        }
    }

    public override void StartAction(string action)
    {
        CurrentWeapon.StartAction(action);

        switch (action)
        {
            case "throw":
                CurrentWeapon = null;
                break; 
        }
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
