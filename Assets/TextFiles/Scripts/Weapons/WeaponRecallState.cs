using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecallState : State, Dependency<Rigidbody2D>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Weapon myWeapon;
    [SerializeField] SendCollision SendCollision;
    [SerializeField] PickupLength PickupLength;
    [SerializeField] private float recallSpeed = 20f;
    [SerializeField] PlayerWeaponDefaultState DefaultState;

    private Rigidbody2D playerRb; 

    public void InjectDependency(Rigidbody2D player)
    {
        playerRb = player; 
    }

    public override void EnterState()
    {
        PickupLength.LengthOfPickup = 0f; 
        myWeapon.AllowPickup();
        transform.parent = null;
        rb.isKinematic = false;
    }

    public void OnPickup()
    {
        StateController.EnterState(DefaultState);
    }

    public override void UpdateState()
    {
        Vector2 newPos;
        float timeToArrive = Vector2.Distance(playerRb.position, rb.position) / recallSpeed;
        newPos = playerRb.position + (playerRb.velocity * timeToArrive);
        rb.velocity = ((newPos - rb.position).normalized * recallSpeed);
    }

    public override void ExitState()
    {
        rb.isKinematic = true;
        SendCollision.StopColliding();
    }
}
