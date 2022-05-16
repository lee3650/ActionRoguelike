using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour, Initializable
{
    [SerializeField] GridController Menu;
    [SerializeField] TimeScaleManager TimeScaleManager;
    [SerializeField] List<TalentPolicy> TestingModules;
    [SerializeField] GameObject UpgradeText;
    [SerializeField] XPManager XPManager;
    [SerializeField] Transform[] PreInitialize;
    [SerializeField] PopupText CompletedModuleText;
    [SerializeField] LevelingManager LevelingManager;
    [SerializeField] UpgradeMenu UpgradeMenu;

    private bool upgradeComplete = false;

    private bool finishedInit = false;

    public void Init()
    {
        finishedInit = false;
        LevelingManager.LevelingManagerReady += LevelingManagerReady;
    }

    private void LevelingManagerReady()
    {
        XPManager.ModuleComplete += ModuleComplete;
        OrderedInit.PerformInitialization(PreInitialize);
        ShowMenu();
        finishedInit = true;
    }

    private void ModuleComplete()
    {
        //During initialization, XP is added to gain talents
        //but the UI should not appear
        if (finishedInit)
        {
            UpgradeText.SetActive(true);
            upgradeComplete = true;
        }
        LevelingManager.UpgradeSelected(XPManager.GetCurrentPolicy());
    }

    private void ShowMenu()
    {
        TimeScaleManager.BeginUntimedFreeze();

        UpgradeText.SetActive(false);
        
        if (upgradeComplete)
        {
            upgradeComplete = false;
            
            CompletedModuleText.Show("Completed " + XPManager.GetCurrentPolicyTitle(), 1f);

            UpgradeMenu.ShowNewOptions();
        } else
        {
            if (!XPManager.HasPolicyInProgress())
            {
                UpgradeMenu.ShowNewOptions();
            }
        }
        
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
                if (XPManager.HasPolicyInProgress())
                {
                    HideMenu();
                }
            }
        }
    }
}
