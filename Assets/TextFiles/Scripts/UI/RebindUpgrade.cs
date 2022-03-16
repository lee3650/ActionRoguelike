using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RebindUpgrade : MonoBehaviour, Dependency<KeyFromTalent>
{
    private KeyFromTalent keyFromTalent; 

    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI KeyBind;

    public void InjectDependency(KeyFromTalent kft)
    {
        keyFromTalent = kft; 
    }

    public void DisplayTalent(TalentPolicy talent)
    {
        Title.text = talent.Title;
        Description.text = talent.Description;

        KeyBind.text = keyFromTalent.GetKeyForActiveTalent(talent.GetComponent<State>());
    }
}
