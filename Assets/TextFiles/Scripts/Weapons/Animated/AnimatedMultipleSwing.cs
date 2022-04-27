using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMultipleSwing : State, Initializable
{
    Weapon MyWeapon;

    [Tooltip("The percent of the swing that the collision should be on for")]
    [SerializeField] [Range(0, 1)] private float ColliderThreshold = 1f;
    [SerializeField] private SendCollision Collider;
    [SerializeField] private TrailRenderer TrailRenderer;
    [SerializeField] State NextState;

    [Tooltip("Alternates swing length then recovery length, starting with swing")]
    [SerializeField] float[] SwingAndRecoveryLengths; 

    private float timer = 0f;

    public void Init()
    {
        MyWeapon = GetComponent<Weapon>();
        print("set my weapon:" + MyWeapon);
    }

    private int currentLength;
    private bool inRecovery; 

    public override void EnterState()
    {
        currentLength = 0;
        inRecovery = false;
        
        SetupAttackState();
        
        timer = 0f;
    }

    private void SetupAttackState()
    {
        MyWeapon.SetAttackStage(AttackStage.Execution);
        Collider.StartColliding();
        TrailRenderer.emitting = true;
    }

    private void SetupRecoveryState()
    {
        MyWeapon.SetAttackStage(AttackStage.Recovery);
        Collider.StopColliding();
        TrailRenderer.emitting = false;
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        bool pastTime = timer >= SwingAndRecoveryLengths[currentLength]; 

        if (inRecovery)
        {
            if (pastTime)
            {
                SetupAttackState();
            }
        } 
        else
        {
            if (pastTime)
            {
                SetupRecoveryState();
            }
            else if (timer >= ColliderThreshold * SwingAndRecoveryLengths[currentLength])
            {
                Collider.StopColliding();
            }
        }

        if (pastTime)
        {
            timer = 0f;
            currentLength++;
            inRecovery = !inRecovery;
            if (currentLength == SwingAndRecoveryLengths.Length)
            {
                StateController.EnterState(NextState);
            }
        }
    }

    public override void ExitState()
    {
        TrailRenderer.emitting = false;
        Collider.StopColliding();
        print("Exited execution!");
    }
}
