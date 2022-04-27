using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEntitySpreadTarget : SpreadTarget
{
    [SerializeField] HandleRecurringEvents HandleRecurringEvents;
    [SerializeField] GenericTarget GenericTarget;
    public override bool CanReceiveSpread(SignalType type)
    {
        return !HandleRecurringEvents.AlreadyHandlingEvent(type);
    }

    public override void ReceiveSpread(GameEvent e)
    {
        GenericTarget.HandleEvent(e);
    }
}
