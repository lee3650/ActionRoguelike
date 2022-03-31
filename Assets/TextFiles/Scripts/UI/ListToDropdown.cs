using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class ListToDropdown
{
    public static List<TMP_Dropdown.OptionData> GetOptionsFromList(IList list)
    {
        List<TMP_Dropdown.OptionData> result = new List<TMP_Dropdown.OptionData>();

        foreach (object o in list) 
        {
            result.Add(new TMP_Dropdown.OptionData(o.ToString()));
        }

        return result; 
    }
}
