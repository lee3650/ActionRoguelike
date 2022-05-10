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

    [SerializeField] List<TalentPolicy> TestingModules;

    public void LateInit()
    {
        DisplayModules(TestingModules);
    }

    public void DisplayModules(List<TalentPolicy> modules)
    {
        foreach (TalentPolicy p in modules)
        {
            TalentGetter module = Instantiate(ModulePrefab, ModuleParent);
            module.Policy = p;
            InjectionSet.InjectDependencies(module.transform);
            OrderedInit.PerformInitialization(module.transform);
            UpgradePoller.AddPoll(module.GetComponent<MousePoll>());
        }
    }
}
