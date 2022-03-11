using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyHitNotifier : MonoBehaviour, Dependency<HitNotifier>, LateInitializable
{
    [SerializeField] MeleeCollisionHandler WeaponCollisionHandler;

    private HitNotifier HitNotifier;

    public void InjectDependency(HitNotifier hn)
    {
        HitNotifier = hn; 
    }

    public void LateInit()
    {
        WeaponCollisionHandler.HitEntity += HitEntity;
    }

    private void HitEntity(Entity obj)
    {
        if (HitNotifier != null)
        {
            HitNotifier.OnHit(obj as Targetable);
        }
    }
}
