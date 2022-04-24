using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionApplyEvents : MonoBehaviour
{
    [SerializeField] WielderSupplier Wielder; 
    [SerializeField] GameEvent[] Templates;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity e))
        {
            foreach (GameEvent temp in Templates)
            {
                temp.Sender = Wielder.GetWielder();
                e.HandleEvent(GameEvent.CopyEvent(temp));
            }
        }
    }
}
