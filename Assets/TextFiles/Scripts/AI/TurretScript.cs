using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour, Dependency<StatsList>
{
    [SerializeField] Factions MyFaction;
    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] InjectionSet ProjectileInjectionSet; 

    //Probably want to read this from like, the TalentPolicy. 
    //Probably wouldn't want to inject this. 
    [SerializeField] Projectile ProjPrefab;
    [SerializeField] string cooldownKey = "cooldown";
    [SerializeField] float Cooldown;

    float timer = 0f;

    Targetable myTarget = null;

    Vector2 lastPos;
    Vector2 prevDelta;

    private StatsList StatsList;

    public void InjectDependency(StatsList sl)
    {
        StatsList = sl; 
    }

    private void SpawnProjectile()
    {
        Projectile p = Instantiate(ProjPrefab, transform.position, transform.rotation);
        ProjectileInjectionSet.InjectDependencies(p.transform);
        p.Launch();
    }

    private void FaceNearestTarget(Targetable target)
    {
        if (target == null || !target.IsAlive())
        {
            return; 
        }
        rb.rotation = UtilityFunctions.GetRotationFromDirection(target.GetMyPosition() - (Vector2)transform.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (myTarget == null || !myTarget.IsAlive())
        {
            myTarget = TargetManager.GetNearestTarget(transform.position, MyFaction);

            //face forward
            Vector2 newDelta = (rb.position - lastPos); 
            if (newDelta.magnitude > 0.01f)
            {
                prevDelta = newDelta; 
            }
            rb.rotation = UtilityFunctions.GetRotationFromDirection(prevDelta);
        }

        FaceNearestTarget(myTarget);

        if (timer > Cooldown && myTarget != null)
        {
            timer = 0f;
            SpawnProjectile();
            
        }

        lastPos = rb.position;

        Cooldown = StatsList.GetStat(cooldownKey);
    }
}
