using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] Transform[] PreInits;
    [SerializeField] UpgradeMenu UpgradeMenu;
    [SerializeField] ProgressionModuleGrid ModuleGrid;
    [SerializeField] ProgressionOptionSupplier OptionSupplier;

    public void Start()
    {
        OrderedInit.PerformInitialization(PreInits);
        ShowMenu();
    }

    public void ShowMenu()
    {
        UpgradeMenu.ShowNewOptions();
        Panel.SetActive(true);
    }

    public void HideMenu()
    {
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
