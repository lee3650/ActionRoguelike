using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCollisionHandler : MonoBehaviour
{
    public abstract void HandleCollision(Collider2D col);
}
