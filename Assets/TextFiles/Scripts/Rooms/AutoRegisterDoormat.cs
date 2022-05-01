using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRegisterDoormat : MonoBehaviour, Initializable
{
    [SerializeField] Doormat Doormat; 

    public void Init()
    {
        Room myRoom = TraverseManager.GetRoom((int)transform.position.x, (int)transform.position.y);

        myRoom.AddDoormat(Doormat);
    }
}
