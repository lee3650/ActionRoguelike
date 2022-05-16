using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : MonoBehaviour, LateInitializable
{
    [SerializeField] GameObject Panel;
    [SerializeField] Transform[] PreInits;
    [SerializeField] UpgradeMenu UpgradeMenu;
    [SerializeField] ProgressionModuleGrid ModuleGrid;
    [SerializeField] UpgradePoller Poller;
    [SerializeField] ProgressionOptionSupplier OptionSupplier;

    public void LateInit()
    {
        OrderedInit.PerformInitialization(PreInits);
        //ShowMenu();
    }

    public void ShowMenu()
    {
        UpgradeMenu.ShowNewOptions();
        Panel.SetActive(true);
    }

    public void HideMenu()
    {
        Poller.ResetPolls();
        Panel.SetActive(false);
    }

    public void RemovePolicy(TalentPolicy tp)
    {
        ModuleGrid.RemovePolicy(tp);
        OptionSupplier.RemovePolicy(tp);
        if (!tp.IsUpgrade)
        {
            UpgradeMenu.ShowNewOptions();
        }
    }
}
