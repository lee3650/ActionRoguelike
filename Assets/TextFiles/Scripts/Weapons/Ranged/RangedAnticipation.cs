using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAnticipation : State, Dependency<HandAndArmGetter>, Initializable
{
    private HandAndArmGetter HandAndArmGetter;

    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] float drawLength;
    [SerializeField] float drawDistance;
    [SerializeField] float placeholderMultiplier = 0.9f;

    [SerializeField] State NextState;

    [SerializeField] GameObject arrowPlaceholder;

    private Vector2 startPlaceholderPos; 

    private float timer = 0f; 

    public void Init()
    {
        startPlaceholderPos = arrowPlaceholder.transform.localPosition;
        MyWeapon.SelectionChanged += SelectionChanged;
    }

    private void SelectionChanged(bool obj)
    {
        arrowPlaceholder.SetActive(obj);
    } 

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

        float t = timer / drawLength; 

        HandAndArmGetter.AnimateHandOut(drawDistance, t);

        arrowPlaceholder.transform.localPosition = Vector2.Lerp(startPlaceholderPos, startPlaceholderPos + new Vector2(-drawDistance * placeholderMultiplier, 0), t);

        if (timer > drawLength)
        {
            StateController.EnterState(NextState);
        }
    }

    public override void ExitState()
    {
        arrowPlaceholder.SetActive(false);
        MyWeapon.SetAttackStage(AttackStage.Execution);
        arrowPlaceholder.transform.localPosition = startPlaceholderPos; 
    }
}
