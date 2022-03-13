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

    private List<UpgradeDisplay> upgradeDisplays;

    public void LateInit()
    {
        LeveledUpPanel.SetActive(false);
        XPManager.LeveledUp += LeveledUp;
    }

    private void LeveledUp()
    {
        Time.timeScale = 0f;
        List<TalentInfo> levels = LevelingManager.GetUpgradeOptions();

        print("leveled up!!!!");

        upgradeDisplays = new List<UpgradeDisplay>();

        foreach (TalentInfo t in levels)
        {
            UpgradeDisplay ud = Instantiate(UpgradeDisplay, UpgradeParent);
            ud.DisplayUpgrade(t, this.UpgradeSelected);
            upgradeDisplays.Add(ud);
        }

        LeveledUpPanel.SetActive(true);
    }

    public void UpgradeSelected(TalentInfo upgrade)
    {
        Time.timeScale = 1f;
        LevelingManager.UpgradeSelected(upgrade);

        foreach (UpgradeDisplay ud in upgradeDisplays)
        {
            Destroy(ud.gameObject);
        }

        upgradeDisplays = new List<UpgradeDisplay>();
    }
}
