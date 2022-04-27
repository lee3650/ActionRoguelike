using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Order: Init(), then SecondInit(), then LateInit(). 
public interface Initializable
{
    void Init();
}
