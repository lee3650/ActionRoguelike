using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : State
{
    [SerializeField] MovementUtility MovementController;
    [SerializeField] DirectionalAnimator DirectionalAnimator;
    [SerializeField] PlayerMoveState PlayerMoveState;
    [SerializeField] PlayerInput Input;
    [SerializeField] HandAndArmGetter HandAndArmGetter;
    [SerializeField] float DodgeSpeed;
    [SerializeField] float DodgeLength;
    [SerializeField] float RecoverySpeed;
    [SerializeField] float RecoveryLength; 
    [SerializeField] float DodgeCooldown;

    private float dodgeTimer = 0f; 

    private float lastDodgeTime = 0;

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
        MovementController.MoveInDirection(dodgeDir, DodgeSpeed);
        dodgeTimer = DodgeLength + RecoveryLength; 
        
        DirectionalAnimator.AnimateRoll(dodgeDir);
        HandAndArmGetter.HideHands();
    }

    public override void UpdateState()
    {
        dodgeTimer -= Time.fixedDeltaTime;

        if (dodgeTimer > RecoveryLength)
        {
            MovementController.MoveInDirection(dodgeDir, DodgeSpeed);
        } else
        {
            MovementController.MoveInDirection(dodgeDir, RecoverySpeed);
        }

        if (dodgeTimer <= 0)
        {
            StateController.EnterState(PlayerMoveState);
        }
    }

    public override void ExitState()
    {
        lastDodgeTime = Time.realtimeSinceStartup;
        HandAndArmGetter.ShowHands();
    }
}
