using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnLOS : MonoBehaviour
{
    [SerializeField] LineOfSight LineOfSight;
    [SerializeField] LineColorLerp LineColorLerp;
    [SerializeField] Projectile Proj;
    [SerializeField] InjectionSet ProjSet;
    [SerializeField] RoomChild RoomChild;
    [SerializeField] float Cooldown;

    float actualCooldown; 

    void FixedUpdate()
    {
        if (!RoomChild.RoomActive())
        {
            return; 
        }

        if (actualCooldown > 0)
        {
            actualCooldown -= Time.fixedDeltaTime;
        }

        if (AttackReady() && LineOfSight.Broken)
        {
            Attack();
        }

        LineColorLerp.LerpLineOpacity(1 - (actualCooldown / Cooldown));
    }

    private void Attack()
    {
        Projectile p = Instantiate(Proj, transform.position, Quaternion.Euler(new Vector3(0, 0, UtilityFunctions.GetRotationFromDirection(transform.up))));
        ProjSet.InjectDependencies(p.transform);
        p.Launch();
        actualCooldown = Cooldown;

        print("attacking! Projectile: " + p);
    }

    private bool AttackReady()
    {
        return actualCooldown - 0.01f <= 0f;
    }
}
