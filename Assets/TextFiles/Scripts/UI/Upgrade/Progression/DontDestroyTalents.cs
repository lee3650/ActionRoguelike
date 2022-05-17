using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTalents : MonoBehaviour
{
    public static int TalentCount = 0;

    [SerializeField] int MyTalentCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        MyTalentCount = TalentCount; 
        TalentCount++;
        this.name = "Talent List: " + MyTalentCount;
        print("Awake called on don't destroy talents!");
    }
}
