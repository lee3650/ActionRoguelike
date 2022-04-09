using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; 

public class SetPlayerClass : MonoBehaviour, Initializable
{
    [SerializeField] TMP_Dropdown ClassOptions;
    [SerializeField] UnlockedClassManager UnlockedClassManager;
    
    public void Init()
    {
        UnlockedClassManager.UpdatedUnlockedClasses += UpdatedUnlockedClasses;
        UpdatedUnlockedClasses(); 
    }

    private void UpdatedUnlockedClasses()
    {
        ClassOptions.options = ListToDropdown.GetOptionsFromList(UnlockedClassManager.GetUnlockedClasses());
    }

    public void DropdownChanged()
    {
        PlayerGetter.CurrentClass = ((PlayerClass[])Enum.GetValues(typeof(PlayerClass)))[ClassOptions.value];
    }

}
