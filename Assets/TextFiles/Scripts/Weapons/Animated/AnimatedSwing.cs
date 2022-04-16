using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSwing : State, Initializable
{
    Weapon MyWeapon;
    
    [Tooltip("The percent of the swing that the collision should be on for")]
    [SerializeField] [Range(0, 1)] private float ColliderThreshold = 1f;
    [SerializeField] private SendCollision Collider;
    [SerializeField] private TrailRenderer TrailRenderer;
    [SerializeField] State NextState;

    [SerializeField] float SwingLength;

    private float actualColThreshold;
    private bool stoppedCollision = false;

    private float timer = 0f; 

    public void Init()
    {
        MyWeapon = GetComponent<Weapon>();
    }

    public override void EnterState()
    {
        MyWeapon.SetAttackStage(AttackStage.Execution);
        stoppedCollision = false;
        Collider.StartColliding();
        TrailRenderer.emitting = true;
        actualColThreshold = ColliderThreshold * SwingLength;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime; 
        if (timer >= actualColThreshold)
        {
            if (!stoppedCollision)
            {
                stoppedCollision = true;
                Collider.StopColliding();
            }
        }
        if (timer >= SwingLength)
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {
        TrailRenderer.emitting = false;
        Collider.StopColliding();
    }
}
