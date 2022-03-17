using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //So... on trigger enter, we basically delete ourselves and increment keys + 1

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KeyManager>(out KeyManager km))
        {
            km.IncrementKeys();
            Destroy(gameObject);
        }       
    }
}
