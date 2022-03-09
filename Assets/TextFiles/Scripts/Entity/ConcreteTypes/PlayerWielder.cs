using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWielder : MonoBehaviour, Wielder
{
    [SerializeField] Factions MyFaction;
    [SerializeField] MovementController MovementController;
    [SerializeField] float hitkb;


    public void HandleEvent(GameEvent e)
    {

    }

    public Transform GetTransform()
    {
        return transform; 
    }

    public bool IsAlive()
    {
        return true; 
    }

    public void OnHitLands(GameObject hit)
    {
        print("Hit " + hit);
        MovementController.AddForce(hitkb, ((Vector2)transform.position - (Vector2)hit.transform.position).normalized);
    }

    public Vector2 GetMyPosition()
    {
        return transform.position; 
    }

    public Factions GetMyFaction()
    {
        return MyFaction; 
    }
}
