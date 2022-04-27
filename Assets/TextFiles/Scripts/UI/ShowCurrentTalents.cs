using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCurrentTalents : MonoBehaviour, Initializable
{
    [SerializeField] PlayerGetter PlayerGetter; 
    [SerializeField] InjectionSet InjectionSet;
    [SerializeField] RebindUpgrade RebindUpgrade;
    [SerializeField] Transform UpgradesParent;
    [SerializeField] GameObject TalentPanel;
    private TalentManager TalentManager;

    private List<RebindUpgrade> previousUpgrades = new List<RebindUpgrade>();

    public void Init()
    {
        PlayerGetter.PlayerReady += PlayerReady;
    }

    private void PlayerReady(Transform obj)
    {
        TalentManager = obj.GetComponent<TalentManager>();
    }

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
