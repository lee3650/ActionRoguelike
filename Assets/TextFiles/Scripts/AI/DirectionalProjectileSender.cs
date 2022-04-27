using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectileSender : MonoBehaviour
{
    [SerializeField] Projectile MyProj;
    [SerializeField] float attackFreq;
    [Tooltip("This is the attack direction in euler angles")]
    [SerializeField] Vector3[] AttackDirections;
    [SerializeField] InjectionSet ProjectileSet; 

    private float timer = 0f;
    private bool attacking = false; 

    public void StartAttack()
    {
        timer = 0f;
        attacking = true;
        print("attacking!");
    }

    public void EndAttack()
    {
        attacking = false;
        print("done attacking!");
    }

    private void FixedUpdate()
    {
        if (!attacking)
        {
            return; 
        }

        timer += Time.fixedDeltaTime; 
        if (timer > attackFreq)
        {
            Attack();
            timer = 0f; 
        }
    }

    private void Attack()
    {
        for (int i = 0; i < AttackDirections.Length; i++)
        {
            Projectile p = Instantiate(MyProj, transform.position, Quaternion.Euler(AttackDirections[i]));
            ProjectileSet.InjectDependencies(p.transform);
            p.Launch();
        }
    }
}
