using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; 

public class SetPlayerClass : MonoBehaviour, Initializable
{
    [SerializeField] TMP_Dropdown ClassOptions; 
    
    public void Init()
    {
        ClassOptions.options = ListToDropdown.GetOptionsFromList(Enum.GetValues(typeof(PlayerClass)));
    }

    public void DropdownChanged()
    {
        PlayerGetter.CurrentClass = ((PlayerClass[])Enum.GetValues(typeof(PlayerClass)))[ClassOptions.value];
    }

}
