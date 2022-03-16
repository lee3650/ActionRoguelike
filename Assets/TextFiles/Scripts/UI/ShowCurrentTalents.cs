using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCurrentTalents : MonoBehaviour
{
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] RebindUpgrade RebindUpgrade;
    [SerializeField] TalentManager TalentManager;
    [SerializeField] Transform UpgradesParent;
    [SerializeField] GameObject TalentPanel;

    private List<RebindUpgrade> previousUpgrades = new List<RebindUpgrade>();

    public void ShowTalents()
    {
        TalentPanel.SetActive(true);

        for (int i = 0; i < previousUpgrades.Count; i++)
        {
            Destroy(previousUpgrades[i].gameObject);
        }
        previousUpgrades = new List<RebindUpgrade>();

        foreach (TalentPolicy tp in TalentManager.GetCurrentTalents())
        {
            RebindUpgrade ru = Instantiate<RebindUpgrade>(RebindUpgrade, UpgradesParent);
            InjectionSet.InjectDependencies(ru.transform);
            previousUpgrades.Add(ru);
            ru.DisplayTalent(tp);
        }
    }

    public void HideTalents()
    {
        TalentPanel.SetActive(false);
    }
}
