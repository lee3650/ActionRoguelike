using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGetter : MonoBehaviour
{
    [SerializeField] TalentPolicy talentPolicy; 

    public TalentPolicy Policy
    {
        get
        {
            return talentPolicy;
        }
    }
}
