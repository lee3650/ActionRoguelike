using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour, Dependency<AbstractCurrentTarget>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float velocity;
    [SerializeField] float updateTime = 0.1f;
    [SerializeField] float BounceLength = 1.5f;

    private float ogUpdateTime = 0f;

    private Targetable target;

    private float timer = 0f;

    public void InjectDependency(AbstractCurrentTarget t)
    {
        ogUpdateTime = updateTime;
        timer = 0f; 
        target = t.ClosestTarget;
    }

    public void BounceBack(bool keepfollowing)
    {
        ogUpdateTime = BounceLength;
        if (!keepfollowing)
        {
            ogUpdateTime = 1000f;
        }
        rb.velocity = -1.25f * rb.velocity; 
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > ogUpdateTime)
        {
            timer = 0f;
            ogUpdateTime = updateTime;
            rb.velocity = (target.GetMyPosition() - (Vector2)transform.position).normalized * velocity;
        }
    }
}
