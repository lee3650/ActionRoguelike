using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEvents : MonoBehaviour, SubEntity
{
    [SerializeField] bool decrementSpreads = true; 

    private List<SignalType> SpreadingEvents = new List<SignalType>();

    private float spreadFreq = 0.16f; 

    public void HandleEvent(GameEvent e)
    {
        if (e.GetStatAsFloat(GameEvent.SpreadsKey, 0) > 0)
        {
            if (SpreadingEvents.Contains(e.Type))
            {
                return; 
            }
            SpreadingEvents.Add(e.Type);
            StartCoroutine(TrySpread(e));
        }
    } 

    private IEnumerator TrySpread(GameEvent e)
    {
        float elapsed = 0f; 

        while (elapsed + spreadFreq < HandleRecurringEvents.RECUR_TIME)
        {
            yield return new WaitForSeconds(spreadFreq);

            List<SpreadTarget> targets = SpreadManager.GetTargets(e.Sender.GetMyFaction(), transform.position, e.Type);
            foreach (SpreadTarget t in targets)
            {
                if (t.transform != transform)
                {
                    if (decrementSpreads)
                    {
                        e.AddToStat(GameEvent.SpreadsKey, -1);
                    }
                    t.ReceiveSpread(e);

                    if (e.GetStatAsFloat(GameEvent.SpreadsKey, 0) == 0)
                    {
                        break;
                    }
                }
            }

            if (e.GetStatAsFloat(GameEvent.SpreadsKey, 0) == 0)
            {
                break;
            }

            elapsed += spreadFreq;
        }

        SpreadingEvents.Remove(e.Type);
    }
}