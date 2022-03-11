using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEmptyObject
{
    public static GameObject GetEmpty()
    {
        return MonoBehaviour.Instantiate<GameObject>(Resources.Load<GameObject>("empty"));
    }
}
