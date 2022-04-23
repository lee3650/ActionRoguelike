using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectOnHit : MonoBehaviour, SubEntity
{
    [SerializeField] Rigidbody2D rb;

    private float lastReflect; 

    public void HandleEvent(GameEvent e)
    {
        //basically if the player sent it, then we should reflect. 
        //we need to make sure we don't reflect multiple times in the same frame 
        if (e.Sender.GetMyFaction() == Factions.Player && Time.realtimeSinceStartup - lastReflect > 1.25f * Time.fixedDeltaTime)
        {
            gameObject.layer = LayerMask.NameToLayer(WeaponLayerMask.PlayerAttackLayer);
            rb.velocity = -rb.velocity;
            lastReflect = Time.realtimeSinceStartup;
        }
    }
}
