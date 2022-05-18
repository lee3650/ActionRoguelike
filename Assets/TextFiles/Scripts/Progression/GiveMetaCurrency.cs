using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMetaCurrency : MonoBehaviour
{
    [SerializeField] int Amt = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MetaCurrencyManager.Balance += Amt;
            Destroy(gameObject);
        }
    }
}
