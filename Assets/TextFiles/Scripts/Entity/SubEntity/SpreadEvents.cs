using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEvents : MonoBehaviour, SubEntity
{
    [SerializeField] GenericTarget GenericTarget;

    public void HandleEvent(GameEvent e)
    {
        if (e.GetStatAsFloat(GameEvent.SpreadsKey, 0) > 0)
        {
            List<SpreadTarget> targets = SpreadManager.GetTargets(GenericTarget.GetMyFaction(), transform.position, e.Type);
            foreach (SpreadTarget t in targets)
            {
                if (t.transform != transform)
                {
                    t.ReceiveSpread(e);
                    e.AddToStat(GameEvent.SpreadsKey, -1);
                    if (e.GetStatAsFloat(GameEvent.SpreadsKey, 0) == 0)
                    {
                        return;
                    }
                }
            }
        }
    } 
}
