using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour, LateInitializable
{
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] TalentGetter ModulePrefab;
    [SerializeField] TalentGetter UpgradePrefab;
    [SerializeField] Transform ModuleParent;
    [SerializeField] UpgradePoller UpgradePoller;
    [SerializeField] LevelingManager LevelingManager;

    public void LateInit()
    {
        //DisplayModules(TestingModules);
    }

    public void DisplayModules(List<TalentPolicy> modules)
    {
        foreach (TalentPolicy p in modules)
        {
            CreateModule(p, ModulePrefab);
        }
    }

    private void CreateModule(TalentPolicy p, TalentGetter prefab)
    {
        TalentGetter module = Instantiate(prefab, ModuleParent);
        module.Policy = p;
        InjectionSet.InjectDependencies(module.transform);
        OrderedInit.PerformInitialization(module.transform);
        UpgradePoller.AddPoll(module.GetComponent<MousePoll>());
    }

    public void DisplayUpgrades(List<TalentPolicy> upgrades)
    {
        foreach (TalentPolicy p in upgrades)
        {
            CreateModule(p, UpgradePrefab);
        }        
    }
}
