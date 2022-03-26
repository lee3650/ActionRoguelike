using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtility : MonoBehaviour
{
    [SerializeField] FacePlayerInput FacePlayerInput;
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] MovementController MovementController;
    [SerializeField] DirectionalAnimator DirectionalAnimator;

    private bool useRotation;
    private float lastRotation; 

    public void StopRotation()
    {
        useRotation = false;
        lastRotation = PlayerInput.GetDirectionToFace();
        WeaponManager.PauseFaceInput();
        FacePlayerInput.SetWiggle(false);
    }

    public void ModifyMoveSpeed(float amt)
    {
        MovementController.ModifyMoveSpeed(amt);
    }
    public void ResetMoveSpeed()
    {
        MovementController.ResetMoveSpeed();
    }

    public PlayerInput GetPlayerInput()
    {
        return PlayerInput;
    }

    public void StartRotation()
    {
        useRotation = true;
        WeaponManager.PlayFaceInput();
        FacePlayerInput.SetWiggle(true);
    }

    public void AddForce(Vector2 dir, float amt)
    {
        MovementController.AddForce(amt, dir);
    }

    public void MoveTowardInput()
    {
        if (useRotation)
        {
            lastRotation = PlayerInput.GetDirectionToFace();
        }

        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());
        
        DirectionalAnimator.AnimateRunDirection(lastRotation, PlayerInput.GetDirectionalInput().sqrMagnitude);
    }
}
