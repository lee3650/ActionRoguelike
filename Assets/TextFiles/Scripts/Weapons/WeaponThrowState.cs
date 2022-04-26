using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowState : State, Dependency<Transform>, Dependency<DirectionSupplier>
{
    [SerializeField] GenericWeapon myWeapon;
    [SerializeField] private SendCollision Collider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float throwSpeed;
    [SerializeField] float spinSpeed;
    [SerializeField] TrailRenderer TrailRenderer;
    [SerializeField] bool enableTrail;
    [SerializeField] GenericCollisionHandler CollisionHandler;
    [SerializeField] State NextState;
    [SerializeField] TagBlacklist TagBlacklist;
    
    private Transform wielder;

    private DirectionSupplier throwDir;

    private bool inState = false; 

    public void InjectDependency(DirectionSupplier throwDirection)
    {
        throwDir = throwDirection; 
    }

    public void InjectDependency(Transform t)
    {
        print("injected wielder: " + t.name);
        wielder = t; 
    }

    public override void EnterState()
    {
        Collider.StartColliding();

        //unparent ourselves, I guess.
        transform.parent = null;
        rb.isKinematic = false;
        rb.velocity = throwDir.GetDir() * throwSpeed;
        rb.angularVelocity = spinSpeed;

        if (enableTrail)
        {
            TrailRenderer.emitting = true; 
        }

        inState = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inState)
        {
            return; 
        }

        if (collision.transform == wielder)
        {
            return; 
        }
        
        if (collision.TryGetComponent<Weapon>(out Weapon w))
        {
            return;
        }

        if (collision.TryGetComponent<Projectile>(out Projectile p))
        {
            return; 
        }

        if (TagBlacklist.IsTagBlacklisted(collision.tag))
        {
            return; 
        }

        CollisionHandler.HandleCollision(collision);

        if (collision.TryGetComponent<Entity>(out Entity e))
        {
            Targetable target = e as Targetable;

            if (target != null)
            {
                if (target.IsAlive())
                {
                    EndThrow(collision.transform);
                }
                else
                {
                    return; 
                }
            }
        }

        EndThrow(collision.transform);
    }

    private void EndThrow(Transform obj)
    {
        print("ending throw from hit to " + obj.name);

        rb.isKinematic = true; 
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        GameObject fakeParent = Instantiate(Resources.Load<GameObject>("empty"));

        fakeParent.transform.position = obj.position; 

        transform.SetParent(fakeParent.transform);

        fakeParent.transform.SetParent(obj);

        myWeapon.FinishedAttack();

        myWeapon.AllowPickup();

        StateController.EnterState(NextState);
    }

    public override void UpdateState()
    {
        //we may want to do something with bounces here, I'm not sure. 
    }

    public override void ExitState()
    {
        if (enableTrail)
        {
            TrailRenderer.emitting = false;
        }
        Collider.StopColliding();

        rb.isKinematic = true;

        inState = false;
    }
}
