using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : GenericWeapon, Dependency<StatsList>, StatListener
{
    [SerializeField] bool UsePlayerStats = false;

    private StatsList playerStats;

    public void InjectDependency(StatsList dependency)
    {
        if (UsePlayerStats)
        {
            playerStats = dependency;
            playerStats.RegisterListener(StatsList.WeaponLengthKey, this);
            StatChanged(StatsList.WeaponLengthKey, playerStats.GetStat(StatsList.WeaponLengthKey));
        }
    }

    public void StatChanged(string stat, float val)
    {
        //width = 1/4 length
        transform.localScale = new Vector3(val, 0.25f * val, transform.localScale.z);

        //0.6f / 1 = the relative scale compared to position (approximately)
        RelativePosition = new Vector2(val * 0.6f, 0);
        transform.localPosition = RelativePosition;
    }
}
