using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerInput : MonoBehaviour
{
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] MovementController MovementController;

    public void FaceInput()
    {
        MovementController.PhysicallyRotateInDirection(PlayerInput.GetDirectionToFace());
    }
}
