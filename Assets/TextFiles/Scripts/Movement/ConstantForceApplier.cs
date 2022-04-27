using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConstantForceApplier : MonoBehaviour
{
    public abstract void AddConstantForce(Vector2 force);
    public abstract void RemoveConstantForce(Vector2 force);
}
