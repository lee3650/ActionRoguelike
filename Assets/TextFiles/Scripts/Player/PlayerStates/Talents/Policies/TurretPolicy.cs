using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPolicy : TalentPolicy, Dependency<InjectionSet>
{
    [SerializeField] GameObject TurretPrefab;
    [SerializeField] InjectionSet MySet; 

    InjectionSet parentSet;

    public void InjectDependency(InjectionSet i)
    {
        parentSet = i; 
    }

    public override void ApplyPolicy()
    {
        GameObject turret = Instantiate(TurretPrefab, transform.position, Quaternion.identity);
        parentSet.InjectDependencies(turret.transform);
        MySet.InjectDependencies(turret.transform);
    }
}
