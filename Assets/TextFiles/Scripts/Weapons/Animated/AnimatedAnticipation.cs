using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedAnticipation : State, Dependency<ReversedTracker>, Initializable
{
    Weapon MyWeapon;
    
    [SerializeField] Animator Animator;
    [SerializeField] State NextState;

    [SerializeField] float AnticipationLength; 

    private const string ReversedAttack = "AttackReversed";
    private const string RegularAttack = "Attack";

    private ReversedTracker ReversedTracker;

    private float timer = 0f; 

    public void Init()
    {
        MyWeapon = GetComponent<Weapon>();
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt; 
    }

    public override void EnterState()
    {
        MyWeapon.SetAttackStage(AttackStage.Anticipation);

        if (ReversedTracker.Reversed)
        {
            Animator.Play(ReversedAttack);
        } else
        {
            Animator.Play(RegularAttack);
        }

        timer = 0f;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime; 
        if (timer >= AnticipationLength)
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {

    }
}
