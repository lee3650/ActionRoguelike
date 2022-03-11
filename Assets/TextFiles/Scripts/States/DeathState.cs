using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Collider2D col;
    [SerializeField] Color deathColor;
    [SerializeField] GameObject hand;

    public override void EnterState()
    {
        col.enabled = false;
        Destroy(hand);
    }

    public override void UpdateState()
    {
        sr.color = deathColor; 
    }
    public override void ExitState()
    {

    }
}
