using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectOnHit : MonoBehaviour, SubEntity
{
    [SerializeField] Rigidbody2D rb; 
    public void HandleEvent(GameEvent e)
    {
        //basically if the player sent it, then we should reflect. 

        if (e.Sender.GetMyFaction() == Factions.Player)
        {
            gameObject.layer = LayerMask.NameToLayer(WeaponLayerMask.PlayerAttackLayer);
            rb.velocity = -rb.velocity; 
        }
    }
}
