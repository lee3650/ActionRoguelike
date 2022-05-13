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

    public void Init()
    {
        LevelingManager.LevelingManagerReady += LevelingManagerReady;
    }

    private void LevelingManagerReady()
    {
        OrderedInit.PerformInitialization(PreInitialize);
        XPManager.ModuleComplete += ModuleComplete;
        ShowMenu();
    }
    
    private void ModuleComplete()
    {
        UpgradeText.SetActive(true);
        upgradeComplete = true;
    }

    private void ShowMenu()
    {
        TimeScaleManager.BeginUntimedFreeze();

        UpgradeText.SetActive(false);
        
        if (upgradeComplete)
        {
            upgradeComplete = false;
            
            CompletedModuleText.Show("Completed " + XPManager.GetCurrentPolicyTitle(), 1f);

            LevelingManager.UpgradeSelected(XPManager.GetCurrentPolicy());

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
