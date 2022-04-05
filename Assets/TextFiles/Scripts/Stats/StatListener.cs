using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StatListener
{
    void StatChanged(string stat, float newVal);
}
