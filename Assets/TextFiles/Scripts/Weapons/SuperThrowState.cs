using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowState : State, Dependency<StatsList>
{
    [SerializeField] SendCollision Collider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float throwSpeed;
    [SerializeField] WielderSupplier WielderSupplier;
    [SerializeField] float spinSpeed;
    [SerializeField] State DefaultState;
    [SerializeField] Weapon MyWeapon;

    private StatsList PlayerStats;

    private int bounces;

    private List<Targetable> targetList;

    private bool wayBack = false; 

    public void InjectDependency(StatsList stats)
    {
        PlayerStats = stats; 
    }

    Vector2 lastPos;

    public override void EnterState()
    {
        transform.parent = null; 
        rb.isKinematic = false;
        bounces = (int)PlayerStats.GetStat("bounces");

        rb.angularVelocity = spinSpeed;

        lastPos = rb.position; 

        wayBack = false;

        Collider.StartColliding();

        targetList = TargetManager.GetNearestTargets(transform.position, Factions.Player);

        if (targetList.Count > bounces)
        {
            for (int i = targetList.Count - 1; i >= bounces; i--)
            {
                targetList.RemoveAt(i);
            }
        }

        targetList.Add(WielderSupplier.GetWielder());

        print("bounces: " + bounces);
        print("targets: " + targetList);
    }

    public void PickedUp()
    {
        StateController.EnterState(DefaultState);
    }

    public override void UpdateState()
    {
        if (targetList.Count == 0)
        {
            //StateController.EnterState(DefaultState);
            if (!wayBack)
            {
                wayBack = true;
                MyWeapon.AllowPickup();
            }
            return;
        }
        if (targetList.Count == 1 && !wayBack)
        {
            wayBack = true;
            MyWeapon.AllowPickup();
        }

        Vector2 newPos;

        Vector2 targetVel = Vector2.zero;
        if (targetList[0].transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D target))
        {
            targetVel = target.velocity;
        }

        print("current target: " + targetList[0]);
        print("target velocity: " + targetVel);
        print("current target position: " + targetList[0].GetMyPosition());

        float timeToArrive = Vector2.Distance(targetList[0].GetMyPosition(), rb.position) / throwSpeed;
        newPos = targetList[0].GetMyPosition() + (targetVel * timeToArrive);
        rb.velocity = ((newPos - rb.position).normalized * throwSpeed);
        if (Vector2.Distance(rb.position, targetList[0].GetMyPosition()) < 0.2f)
        {
            targetList.RemoveAt(0);
            print("removed from distance!");
        } else
        {
            //project the target onto the line of our last movement
            Vector2 moveDir = rb.position - lastPos; 
            Vector2 proj = Vector3.Project(targetList[0].GetMyPosition() - lastPos, moveDir);

            print("movement direction: " + moveDir);

            print("projected point: " + proj);
            //blue = movement line
            Debug.DrawLine(lastPos, rb.position, Color.blue, 10f);
            //yellow = projection of target pos onto movement line
            proj += lastPos;
            
            print("adjust proj point: " + proj);
            Debug.DrawLine(proj, targetList[0].GetMyPosition(), Color.yellow, 10f);

            //if the point is inside the two lines, check the distance
            float ab = Vector2.Dot(moveDir, moveDir);
            float ac = Vector2.Dot(moveDir, (proj - lastPos));
            if (ac > 0 && ac < ab)
            {
                //I feel like we should add something to proj - we should add lastPos to it, right? 
                //inside the two lines. This should not be common. 
                if (Vector2.Distance(proj, targetList[0].GetMyPosition()) < 0.2f) {
                    targetList.RemoveAt(0);
                    print("removed from projection!");
                }
            }
        }

        lastPos = rb.position; 
    }

    public override void ExitState()
    {
        wayBack = false; 
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f; 
        Collider.StopColliding();
    }
}
