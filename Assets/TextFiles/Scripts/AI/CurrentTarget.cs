using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTarget : AbstractCurrentTarget, LateInitializable
{
    [SerializeField] bool searching;
    [SerializeField] float SearchDelay = 0.1f;

    private Targetable MyTargetable; 

    public void LateInit()
    {
        MyTargetable = GetComponent<Targetable>();

        StartCoroutine(SearchForTarget());
    }

    IEnumerator SearchForTarget()
    {
        while (true)
        {
            if (searching)
            {
                Targetable candidate = TargetManager.GetNearestTarget(transform.position, MyTargetable.GetMyFaction());

                if (candidate != ClosestTarget)
                {
                    ClosestTarget = candidate; 
                }
            }

            yield return new WaitForSeconds(SearchDelay);
        }
    }

}
