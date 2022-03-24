using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerInput : MonoBehaviour
{
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] MovementController MovementController;
    [SerializeField] bool alsoWiggle;
    [SerializeField] float wiggleAmount, wiggleFreq;
    [SerializeField] Rigidbody2D playerRb;
    private float timer = 0f;
    
    public void FaceInput()
    {
        float adjustment = 0; 
        if (alsoWiggle && playerRb.velocity.magnitude > 1f)
        {
            timer += Time.fixedDeltaTime;
            adjustment = Mathf.LerpAngle(-wiggleAmount, wiggleAmount, timer * wiggleFreq);
            if (timer * wiggleFreq >= 1)
            {
                timer = 0f; 
                wiggleAmount = -wiggleAmount;
            }
            //adjustment = wiggleAmount * Mathf.Sin(2 * Mathf.PI * timer * wiggleFreq);
        }

        MovementController.PhysicallyRotateInDirection(PlayerInput.GetDirectionToFace() + adjustment);
    }
}
