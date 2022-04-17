using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTarget : MonoBehaviour, LateInitializable
{
    [SerializeField] bool searching;
    [SerializeField] float SearchDelay = 0.1f;

    private Targetable MyTargetable; 

    public void LateInit()
    {
        MyTargetable = GetComponent<Targetable>();

        StartCoroutine(SearchForTarget());
    }

    public Vector2 GetTargetPosition()
    {
        return ClosestTarget.GetMyPosition();
    }

    public Targetable ClosestTarget
    {
        get;
        private set; 
    }

    public bool HasTarget
    {
        get
        {
            return ClosestTarget != null && ClosestTarget.IsAlive();
        }
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
