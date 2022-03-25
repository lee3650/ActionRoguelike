using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : State
{
    [SerializeField] MovementController MovementController;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] PlayerInput Input;
    [SerializeField] float DodgeSpeed;
    [SerializeField] float DodgeLength;
    [SerializeField] float RecoverySpeed;
    [SerializeField] float RecoveryLength; 
    [SerializeField] float DodgeCooldown;

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
        if (dodgeDir == Vector2.zero)
        {
            float z = Input.GetDirectionToFace();
            Vector2 rounded = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
            rounded = new Vector2(Mathf.RoundToInt(rounded.x), Mathf.RoundToInt(rounded.y));
            dodgeDir = rounded.normalized;
        }
        MovementController.AddForce(DodgeSpeed, dodgeDir);
        rotateDir = UtilityFunctions.GetRotationFromDirection(dodgeDir);
        dodgeTimer = DodgeLength + RecoveryLength; 
    }

    public override void UpdateState()
    {
        dodgeTimer -= Time.fixedDeltaTime;

        if (dodgeTimer > RecoveryLength)
        {
            MovementController.AddForce(DodgeSpeed, dodgeDir);
        } else
        {
            MovementController.AddForce(RecoverySpeed, dodgeDir);
        }

        //MovementController.RotateInDirection(rotateDir);

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
