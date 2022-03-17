using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LevelupPanel : MonoBehaviour, LateInitializable
{
    [SerializeField] UpgradeDisplay UpgradeDisplay;
    [SerializeField] XPManager XPManager;
    [SerializeField] GameObject LeveledUpPanel;
    [SerializeField] Transform UpgradeParent;
    [SerializeField] LevelingManager LevelingManager;
    [SerializeField] TimeScaleManager TimeScaleManager;
    [SerializeField] KeyFromTalent KeyFromTalent;

    private List<UpgradeDisplay> upgradeDisplays;

    public void LateInit()
    {
        LeveledUpPanel.SetActive(false);
        XPManager.LeveledUp += LeveledUp;
    }

    private void LeveledUp()
    {
        TimeScaleManager.BeginUntimedFreeze();
        List<TalentPolicy> levels = LevelingManager.GetUpgradeOptions();

        upgradeDisplays = new List<UpgradeDisplay>();

        foreach (TalentPolicy t in levels)
        {
            UpgradeDisplay ud = Instantiate(UpgradeDisplay, UpgradeParent);
            ud.DisplayUpgrade(t, this.UpgradeSelected, KeyFromTalent);
            upgradeDisplays.Add(ud);
        }

        LeveledUpPanel.SetActive(true);
    }

    public void UpgradeSelected(TalentPolicy upgrade)
    {
        LevelingManager.UpgradeSelected(upgrade);

        foreach (UpgradeDisplay ud in upgradeDisplays)
        {
            Destroy(ud.gameObject);
        }

        LeveledUpPanel.SetActive(false);

        upgradeDisplays = new List<UpgradeDisplay>();

        TimeScaleManager.EndUntimedFreeze();
    }
}
