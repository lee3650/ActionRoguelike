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

    private bool upgradeComplete = false;

    private List<TalentPolicy> PolicyOptions;

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

    public void ShowPreviousOptions()
    {
        Menu.DisplayModules(PolicyOptions);
    }

    public void ShowUpgrades(TalentPolicy policy)
    {
        Menu.DisplayUpgrades(LevelingManager.GetUpgradesForTalent(policy));
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

            PolicyOptions = LevelingManager.GetUpgradeOptions();
            Menu.DisplayModules(PolicyOptions);
        } else
        {
            if (!XPManager.HasPolicyInProgress())
            {
                PolicyOptions = LevelingManager.GetUpgradeOptions();
                Menu.DisplayModules(PolicyOptions);
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
