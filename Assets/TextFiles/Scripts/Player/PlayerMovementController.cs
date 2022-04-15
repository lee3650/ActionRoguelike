using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : AbstractMovementController, LateInitializable, StatListener
{
    [SerializeField] StatsList StatsList;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float sensitivity = 0.9f;
    [SerializeField] float initialSens = 0.5f; 
    [SerializeField] float decay = 0.95f;

    private Vector2 lastDir = new Vector2();

    private Vector2 momentum = new Vector2();

    private float lastForce = 0f;

    public void LateInit()
    {
        baseMoveSpeed = StatsList.GetStat(speedStat); 
        effectiveMoveSpeed = baseMoveSpeed;
        StatsList.RegisterListener(speedStat, this);
    }

    public void StatChanged(string s, float v)
    {
        baseMoveSpeed = v;
        effectiveMoveSpeed = v; 
    }

    public override void AddForce(float force, Vector2 dir)
    {
        momentum += force * dir;
        rb.velocity = momentum;
        lastForce = Time.realtimeSinceStartup; 
    }

    public void MoveInDirection(Vector2 dir, float vel)
    {
        rb.velocity = (dir * vel) + momentum;
    }

    public override void MoveInDirection(Vector2 dir)
    {
        if (Time.realtimeSinceStartup - lastForce > Time.fixedDeltaTime)
        {
            if (dir != new Vector2(0, 0)) // && lastDir != new Vector2(0, 0)
            {
                float sens = sensitivity; 
                if (lastDir == new Vector2(0, 0))
                {
                    sens = initialSens;
                } 
                dir = Vector2.Lerp(lastDir, dir, sens);
            } 
            rb.velocity = (dir * effectiveMoveSpeed) + momentum;
            lastDir = dir;
        }
    }

    void FixedUpdate()
    {
        momentum *= decay;
        if (momentum.magnitude < 0.01)
        {
            momentum = new Vector2(); 
        }
    }
}
