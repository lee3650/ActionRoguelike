using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitNotifier : MonoBehaviour
{
    public abstract void OnHit(Targetable hit);
}
