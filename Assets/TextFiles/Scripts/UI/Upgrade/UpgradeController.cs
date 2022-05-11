using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour, LateInitializable
{
    [SerializeField] GridController Menu;
    [SerializeField] TimeScaleManager TimeScaleManager;
    [SerializeField] List<TalentPolicy> TestingModules;
    [SerializeField] GameObject UpgradeText;
    [SerializeField] XPManager XPManager;
    [SerializeField] Transform[] PreInitialize;

    public void LateInit()
    {
        OrderedInit.PerformInitialization(PreInitialize);
        ShowMenu();
        XPManager.ModuleComplete += ModuleComplete;
    }
    private void ModuleComplete()
    {
        UpgradeText.SetActive(true); 
    }

    private void ShowMenu()
    {
        TimeScaleManager.BeginUntimedFreeze();
        Menu.DisplayModules(TestingModules);
        Menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.gameObject.SetActive(false);
        UpgradeText.SetActive(false);
        TimeScaleManager.EndUntimedFreeze();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Menu.gameObject.activeInHierarchy)
            {
                ShowMenu();
            }
            else
            {
                if (XPManager.HasCurrentPolicy() && XPManager.GetXPPercentage() < 1)
                {
                    HideMenu();
                }
            }
        }
    }
}
