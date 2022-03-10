using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTaker : MonoBehaviour, SubEntity
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float kbScalar; 

    public void HandleEvent(GameEvent g)
    {
        switch (g.Type)
        {
            case SignalType.Physical:
                TakeKnockback(g.Sender.GetMyPosition(), kbScalar);
                break;
        }
    }

    private void TakeKnockback(Vector2 source, float amt)
    {
        Vector2 delta = ((Vector2)transform.position - source).normalized;
        rb.AddForce(delta * amt);
    }
}