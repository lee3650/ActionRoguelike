using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEventHandler : TalentPolicy, Dependency<GenericTarget>, Initializable
{
    [SerializeField] Component modifier;
    [SerializeField] TalentPolicy[] Upgrades; 
    GenericTarget MyTarget;
    EventModifier myModifier;

    private int nextUpgrade = 0; 

    public void InjectDependency(GenericTarget gt)
    {
        MyTarget = gt; 
    }

    public EventModifier GetMyModifier()
    {
        return myModifier;
    }

    public void Init()
    {
        myModifier = modifier as EventModifier; 
    }

    public override TalentPolicy GetNextUpgrade()
    {
        return Upgrades[nextUpgrade];
    }

    public override void AppliedNextUpgrade()
    {
        nextUpgrade++;
        if (nextUpgrade == Upgrades.Length)
        {
            Upgradable = false;
        }
    }

    public override void ApplyPolicy()
    {
        MyTarget.AddEventModifier(myModifier);
    }
}
