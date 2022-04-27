using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashOnDamage : MonoBehaviour, SubEntity
{
    [SerializeField] SquashAndStretch SquashAndStretch;
    public void HandleEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case SignalType.Physical:
                SquashAndStretch.StartSquash();
                break;
        }
    }
}
