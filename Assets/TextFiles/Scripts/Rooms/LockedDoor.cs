using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
    [SerializeField] GameObject lockGraphic; 
    private bool open = true;
    private bool locked = true; 

    public override void Open()
    {
        open = true; 
        if (!locked)
        {
            base.Open();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (open && locked)
        {
            if (collision.transform.TryGetComponent<KeyManager>(out KeyManager km))
            {
                if (km.HasKey())
                {
                    km.DecrementKeys();
                    locked = false;
                    Destroy(lockGraphic);
                    lockGraphic = null;
                    base.Open();
                }
            }
        }
    }

    public override void Close()
    {
        base.Close();
        open = false; 
    }
}
