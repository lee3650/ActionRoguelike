using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParent : MonoBehaviour
{
    [SerializeField] Transform Transform;
    void Start()
    {
        transform.SetParent(Transform);
    }
}
