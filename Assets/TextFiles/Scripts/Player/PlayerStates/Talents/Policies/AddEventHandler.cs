using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEventHandler : TalentPolicy, Dependency<GenericTarget>, Initializable
{
    [SerializeField] Component modifier; 
    GenericTarget MyTarget;
    EventModifier myModifier;

    public void InjectDependency(GenericTarget gt)
    {
        MyTarget = gt; 
    }

    public void Init()
    {
        myModifier = modifier as EventModifier; 
    }

    public override void ApplyPolicy()
    {
        MyTarget.AddEventModifier(myModifier);
    }

    public override void ApplyUpgrade(int index)
    {

    }

    public override TalentInfo GetNextUpgradeInfo()
    {
        return null; //?
    }
}
