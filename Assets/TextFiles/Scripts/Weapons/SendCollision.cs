using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCollision : MonoBehaviour
{
    [SerializeField] WeaponCollisionHandler CollisionHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionHandler.HandleCollision(collision);
    }
}
