using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectStateController : MonoBehaviour, SecondInitializable
{
    [SerializeField] StateController controller; 

    public void SecondInit()
    {
        foreach (State s in GetComponents<State>())
        {
            s.SetStateController(controller);
        }
    }
}
