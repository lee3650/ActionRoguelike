using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal Connection; 
    //this is the list of transforms to not transport back when they collide 
    private List<Transform> Travelers = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Teleportable>(out Teleportable c))
        {
            if (!c.CanTeleport())
            {
                return;
            }
        }

        if (Travelers.Contains(collision.transform))
        {
            Travelers.Remove(collision.transform);
            return; 
        }

        Connection.AddTraveler(collision.transform);
        collision.transform.position = Connection.transform.position;
    }

    public void AddTraveler(Transform t)
    {
        Travelers.Add(t);
    }
}
