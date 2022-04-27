using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTaker : MonoBehaviour, SubEntity
{
    [SerializeField] TakeKnockback takeKb;
    [SerializeField] float kbScalar; 

    public void HandleEvent(GameEvent g)
    {
        switch (g.Type)
        {
            case SignalType.Physical:
                TakeKnockback(g.Sender.GetMyPosition(), kbScalar);
                break;
            case SignalType.Knockback:
                TakeKnockback(g.Sender.GetMyPosition(), g.Magnitude);
                break; 
        }
    }

    private void TakeKnockback(Vector2 source, float amt)
    {
        Vector2 delta = ((Vector2)transform.position - source).normalized;
        takeKb.ApplyKnockback(amt, delta);
    }
}