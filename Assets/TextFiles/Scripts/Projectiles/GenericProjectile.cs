using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectile : Projectile, Dependency<WeaponCollisionHandler>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] Collider2D col;
    [SerializeField] bool DestroyOnImpact = false;
    [SerializeField] TagBlacklist TagBlacklist;

    private WeaponCollisionHandler colHandler; 

    public void InjectDependency(WeaponCollisionHandler wc)
    {
        colHandler = wc; 
    }

    public override void Launch()
    {
        rb.velocity = speed * transform.right; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagBlacklist.IsTagBlacklisted(collision.tag))
        {
            return; 
        }

        //should we do the same thing where it goes through stuff it kills? 
        colHandler.HandleCollision(collision);

        if (collision.TryGetComponent<Targetable>(out Targetable t))
        {
            if (t.IsAlive())
            {
                StickToTarget(collision.transform);
            }
        } else
        {
            if (collision.GetComponent<Weapon>() == null)
            {
                StickToTarget(collision.transform);
            }
        }
    }

    private void StickToTarget(Transform target)
    {
        col.enabled = false;
        rb.velocity = Vector2.zero;

        rb.isKinematic = true; 

        if (DestroyOnImpact)
        {
            gameObject.SetActive(false);
        }

        //duplication with weapon throw state
        GameObject fakeParent = Instantiate(Resources.Load<GameObject>("empty"));
        fakeParent.transform.position = target.position;
        transform.SetParent(fakeParent.transform);
        fakeParent.transform.SetParent(target);
    }
}
