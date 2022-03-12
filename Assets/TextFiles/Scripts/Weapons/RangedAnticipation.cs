using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAnticipation : State, Dependency<HandAndArmGetter>
{
    private HandAndArmGetter HandAndArmGetter;

    [SerializeField] Weapon MyWeapon;
    [SerializeField] float drawLength;
    [SerializeField] float drawDistance;

    [SerializeField] State NextState;

    [SerializeField] GameObject arrowPlaceholder; 

    private float timer = 0f; 

    public void InjectDependency(HandAndArmGetter h)
    {
        HandAndArmGetter = h;
    }

    public override void EnterState()
    {
        print("entered ranged anticipation!");
        MyWeapon.SetAttackStage(AttackStage.Anticipation);
        arrowPlaceholder.SetActive(true);
        timer = 0f; 
    }
    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        HandAndArmGetter.AnimateHandOut(drawDistance, timer / drawLength);

        if (timer > drawLength)
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {
        arrowPlaceholder.SetActive(false);
        MyWeapon.SetAttackStage(AttackStage.Execution);
    }
}
