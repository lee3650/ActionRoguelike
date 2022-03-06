using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCollision : MonoBehaviour
{
    [SerializeField] WeaponCollisionHandler CollisionHandler;
    [SerializeField] Collider2D myCol; 

    private Vector2 lastPos = Vector2.zero;

    private bool colliding = false; 

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

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, myCol.bounds.size, 0f, delta.normalized, delta.magnitude);

        Debug.DrawLine(lastPos, transform.position, Color.red, 10f);

        foreach (RaycastHit2D hit in hits)
        {
            CollisionHandler.HandleCollision(hit.collider);
        }

        lastPos = transform.position; 
    }
}
