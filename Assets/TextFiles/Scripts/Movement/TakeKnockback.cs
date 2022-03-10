using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeKnockback : MonoBehaviour
{
    [SerializeField] MovementController MovementController;

    public void ApplyKnockback(float amt, Vector2 dir)
    {
        MovementController.AddForce(amt, dir);
    }
}
