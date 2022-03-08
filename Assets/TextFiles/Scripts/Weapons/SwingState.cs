using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingState : AbstractSwing, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>
{
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

    public override void EnterState()
    {
        SetupState();
        Collider.StartColliding();
        TrailRenderer.emitting = true;
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {
        Collider.StopColliding();

        TrailRenderer.emitting = false; 
    }
}
