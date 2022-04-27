using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPolicy : TalentPolicy, Dependency<InjectionSet>
{
    [SerializeField] GameObject TurretPrefab;
    [SerializeField] InjectionSet MySet;

    private GameObject spawnedTurret; 

    InjectionSet parentSet;

    public void InjectDependency(InjectionSet i)
    {
        parentSet = i; 
    }

    public override void ApplyPolicy()
    {
        spawnedTurret = Instantiate(TurretPrefab, transform.position, Quaternion.identity);
        parentSet.InjectDependencies(spawnedTurret.transform);
        MySet.InjectDependencies(spawnedTurret.transform);
    }

    public override void UndoPolicy()
    {
        Destroy(spawnedTurret);
        RemoveTalentAndUndoUpgrades();
    }
}
