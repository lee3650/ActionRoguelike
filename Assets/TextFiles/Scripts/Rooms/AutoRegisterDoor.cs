using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRegisterDoor : MonoBehaviour, Initializable
{
    [SerializeField] Door d;
    public void Init()
    {
        //x and y should already be integers
        Room r = TraverseManager.GetRoom((int)transform.position.x, (int)transform.position.y);

        Prereq.Assert(r != null, string.Format("The room at point ({0}, {1}) was null!", transform.position.x, transform.position.y));

        r.AddDoor(d);
    }
}
