using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCollision : MonoBehaviour, LateInitializable
{
    [SerializeField] WeaponCollisionHandler CollisionHandler;
    [SerializeField] Collider2D myCol;
    [SerializeField] WeaponFaction WeaponFaction;

    private Vector2 lastPos = Vector2.zero;

    private bool colliding = false;

    private LayerMask mask; 

    public void LateInit()
    {
        mask = WeaponLayerMask.GetLayerMask(WeaponFaction.GetFaction());
    }

    public void StartColliding()
    {
        colliding = true; 
        lastPos = transform.position;
    }

    public void StopColliding()
    {
        colliding = false; 
    }

    private void FixedUpdate()
    {
        if (!colliding)
        {
            return; 
        }

        Vector2 delta = lastPos - (Vector2)transform.position;

        //technically this depends on faction... hm... we should probably inject it then 

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, myCol.bounds.size, transform.localEulerAngles.z, delta.normalized, delta.magnitude, mask);

        //Debug.DrawLine(lastPos, transform.position, Color.red, 10f);

        foreach (RaycastHit2D hit in hits)
        {
            CollisionHandler.HandleCollision(hit.collider);
        }

        lastPos = transform.position; 
    }
}
