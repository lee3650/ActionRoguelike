using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEvents : MonoBehaviour, SubEntity
{
    [SerializeField] GenericTarget GenericTarget;

    public void HandleEvent(GameEvent e)
    {
        if (e.Spreads > 0)
        {
            List<SpreadTarget> targets = SpreadManager.GetTargets(GenericTarget.GetMyFaction(), transform.position, e.Type);
            foreach (SpreadTarget t in targets)
            {
                if (t.transform != transform)
                {
                    t.ReceiveSpread(e);
                    e.Spreads--;
                    if (e.Spreads == 0)
                    {
                        return;
                    }
                }
            }
        }
    } 
}
