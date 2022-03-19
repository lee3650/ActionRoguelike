using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] Factions MyFaction;
    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] InjectionSet ProjectileInjectionSet; 

    //Probably want to read this from like, the TalentPolicy. 
    //Probably wouldn't want to inject this. 
    [SerializeField] Projectile ProjPrefab;
    [SerializeField] float Cooldown;

    float timer = 0f;

    Targetable myTarget = null; 

    private void SpawnProjectile()
    {
        Projectile p = Instantiate(ProjPrefab, transform.position, transform.rotation);
        ProjectileInjectionSet.InjectDependencies(p.transform);
        p.Launch();
    }

    private void FaceNearestTarget(Targetable target)
    {
        if (target == null)
        {
            return; 
        }
        rb.rotation = KeyboardInput.GetRotationFromDirection(target.GetMyPosition() - (Vector2)transform.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (myTarget == null || !myTarget.IsAlive())
        {
            myTarget = TargetManager.GetNearestTarget(transform.position, MyFaction);
        }

        FaceNearestTarget(myTarget);

        if (timer > Cooldown && myTarget != null)
        {
            timer = 0f;
            SpawnProjectile();
            
        }
    }
}
