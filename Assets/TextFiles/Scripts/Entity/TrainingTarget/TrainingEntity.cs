using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingEntity : MonoBehaviour, Entity
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float kbAmount;
    public void HandleEvent(GameEvent e) 
    {
        Vector2 delta = ((Vector2)transform.position - e.Sender.GetMyPosition()).normalized;
        rb.AddForce(kbAmount * delta); 
    }

    public Transform GetTransform()
    {
        return transform; 
    }
}
