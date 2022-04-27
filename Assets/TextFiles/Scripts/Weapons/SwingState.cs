using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingState : AbstractSwing, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>
{
    [SerializeField] Weapon MyWeapon;
    [Tooltip("The percent of the swing that the collision should be on for")]
    [SerializeField] [Range(0, 1)] private float ColliderThreshold = 1f;
    [SerializeField] private SendCollision Collider;
    [SerializeField] private TrailRenderer TrailRenderer;

    public void InjectDependency(ReversedTracker reversedTracker)
    {
        ReversedTracker = reversedTracker;
    }

    public void InjectDependency(HandAndArmGetter handAndArmGetter)
    {
        HandAndArm = handAndArmGetter; 
    }

    private bool stoppedCollision = false;
    private float actualCollisionThreshold = 0f;

    public override void EnterState()
    {
        SetupState();

        actualCollisionThreshold = ColliderThreshold * SwingLength; 

        stoppedCollision = false;
        Collider.StartColliding();
        TrailRenderer.emitting = true;
        MyWeapon.SetAttackStage(AttackStage.Execution);
    }

    public override void UpdateState()
    {
        PartialUpdate();
        if (timer > actualCollisionThreshold && !stoppedCollision)
        {
            Collider.StopColliding();
            stoppedCollision = true; 
        }
    }

    public override void ExitState()
    {
        MyWeapon.SetAttackStage(AttackStage.Recovery);
        Collider.StopColliding();
        TrailRenderer.emitting = false; 
    }
}
