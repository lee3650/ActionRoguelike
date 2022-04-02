using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowState : State, Dependency<StatsList>, Dependency<DirectionSupplier>, Dependency<TakeKnockback>
{
    [SerializeField] SendCollision Collider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float throwSpeed;
    [SerializeField] WielderSupplier WielderSupplier;
    [SerializeField] float spinSpeed;
    [SerializeField] State DefaultState;
    [SerializeField] GenericWeapon MyWeapon;
    [SerializeField] GenericCollisionHandler GenericCollisionHandler;

    private DirectionSupplier ds;

    private StatsList PlayerStats;

    private int bounces;

    private List<Targetable> targetList;

    private bool wayBack = false;
    private bool inState = false;
    private TakeKnockback TK; 

    public void InjectDependency(TakeKnockback tk)
    {
        TK = tk; 
    }

    public void InjectDependency(StatsList stats)
    {
        PlayerStats = stats; 
    }

    public void InjectDependency(DirectionSupplier dir)
    {
        ds = dir; 
    }

    //Vector2 lastPos;

    public override void EnterState()
    {
        transform.parent = null; 
        rb.isKinematic = false;
        bounces = (int)PlayerStats.GetStat("bounces");

        rb.angularVelocity = spinSpeed;

        //lastPos = rb.position; 

        wayBack = false;

        Collider.StartColliding();

        targetList = new List<Targetable>();

        inState = true; 

        //targetList = TargetManager.GetNearestTargets(transform.position, Factions.Player);

        /*
        if (targetList.Count > bounces)
        {
            for (int i = targetList.Count - 1; i >= bounces; i--)
            {
                targetList.RemoveAt(i);
            }
        }
         */

        rb.velocity = ds.GetDir() * throwSpeed; 

        targetList.Add(WielderSupplier.GetWielder());

        print("bounces: " + bounces);
        print("targets: " + targetList);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (inState && !col.TryGetComponent<Entity>(out Entity e) && !col.TryGetComponent<Weapon>(out Weapon w) && !wayBack)
        {
            //Process: take the difference between the normal and the velocity
            //and rotate the normal by that amount. 

            Vector2 closestPoint = col.ClosestPoint(transform.position);
            Vector2 normal = (Vector2)transform.position - closestPoint;
            
            if (Vector2.Dot(normal, rb.velocity) > 0) 
            {
                //if they're going in the same direction, then we should ignore this collision
                //because the weapon is traveling away from the wall
                print("The weapon was traveling away from the collision when it hit!");
                return; 
            }

            normal.Normalize();

            Debug.DrawLine(closestPoint, normal + closestPoint, Color.red, 10f);

            Debug.DrawLine(transform.position, (Vector2)transform.position + rb.velocity.normalized, Color.blue, 10f);

            Vector2 projection = Vector3.Project(rb.velocity.normalized, -normal);

            Vector2 dif = rb.velocity.normalized - projection;
            Vector2 newDir = dif + normal;

            Debug.DrawLine(closestPoint, closestPoint + projection, Color.green, 10f);

            Debug.DrawLine(closestPoint, closestPoint + projection, Color.green, 10f);

            if (col.bounds.Contains(transform.position))
            {
                //then we need to invert the direction, since the normal is going to be backwards. 
                newDir = -newDir;
            }

            rb.velocity = newDir.normalized * throwSpeed;

            bounces--; 
            if (bounces <= 0)
            {
                wayBack = true;
                MyWeapon.AllowPickup();
            }

            GenericCollisionHandler.ResetHitEntities();
        }
    }

    public void PickedUp()
    {
        TK.ApplyKnockback(1500, rb.velocity.normalized);
        StateController.EnterState(DefaultState);
    }

    public override void UpdateState()
    {
        Vector2 newPos;

        if (wayBack)
        {
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
        }
    }

    public override void ExitState()
    {
        wayBack = false; 
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f; 
        Collider.StopColliding();
        MyWeapon.FinishedAttack();
        inState = false; 
    }
}
