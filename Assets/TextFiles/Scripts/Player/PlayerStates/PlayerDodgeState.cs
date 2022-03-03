using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : State
{
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] PlayerInput Input;
    [SerializeField] float DodgeSpeed;
    [SerializeField] float DodgeCooldown;
    [SerializeField] float DodgeLength;

    private float dodgeTimer = 0f; 

    private float lastDodgeTime = 0;

    private float rotateDir = 0;

    private Vector2 dodgeDir; 

    public bool CanDodge()
    {
        return Time.realtimeSinceStartup - lastDodgeTime > DodgeCooldown; 
    }

    public override void EnterState()
    {
        dodgeDir = Input.GetDirectionalInput();
        MovementController.AddForce(DodgeSpeed, dodgeDir);
        rotateDir = KeyboardInput.GetRotationFromDirection(dodgeDir);
        dodgeTimer = DodgeLength; 
    }

    public override void UpdateState()
    {
        dodgeTimer -= Time.deltaTime;

        MovementController.AddForce(DodgeSpeed, dodgeDir);

        MovementController.RotateInDirection(rotateDir);

        if (dodgeTimer <= 0)
        {
            StateController.EnterState(PlayerMoveState);
        }
    }

    public override void ExitState()
    {
        lastDodgeTime = Time.realtimeSinceStartup;
    }
}
