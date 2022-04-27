using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] Vector2 direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ConstantForceApplier>(out ConstantForceApplier cfa))
        {
            cfa.AddConstantForce(force * direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ConstantForceApplier>(out ConstantForceApplier cfa))
        {
            cfa.RemoveConstantForce(force * direction);
        }
    }
}
