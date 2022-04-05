using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockStatsListener : StatListener
{
    public Dictionary<string, float> Stats = new Dictionary<string, float>();

    public void StatChanged(string stat, float val)
    {
        Stats[stat] = val;
    }
}
