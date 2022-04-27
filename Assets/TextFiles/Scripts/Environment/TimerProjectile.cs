using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerProjectile : MonoBehaviour, LateInitializable
{
    [SerializeField] RoomChild RoomChild;
    [SerializeField] float Cooldown;
    [SerializeField] Projectile proj;
    [SerializeField] InjectionSet InjectionSet;

    float timer = 0f;

    private bool active = false;

    public void LateInit()
    {
        RoomChild.RoomEntered += RoomEntered;
    }

    private void RoomEntered()
    {
        active = true;
        timer = Cooldown;
    }

    private void FixedUpdate()
    {
        if (!active || !RoomChild.RoomActive())
        {
            return;
        }

        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            timer = Cooldown;
            Attack();
        }
    }

    public static void LaunchProjectile(Projectile proj, Vector2 pos, Vector2 dir, InjectionSet injectionSet)
    {
        Projectile p = Instantiate(proj, pos, Quaternion.Euler(new Vector3(0, 0, UtilityFunctions.GetRotationFromDirection(dir))));
        injectionSet.InjectDependencies(p.transform);
        p.Launch();
    }

    private void Attack()
    {
        LaunchProjectile(proj, transform.position, transform.up, InjectionSet);
        print("timer is attacking");
    }
}
