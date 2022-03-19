using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRecurringEvents : MonoBehaviour, SubEntity, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] float recurrenceTime = 0.75f;
    [SerializeField] GenericTarget MyTarget;
    private List<GameEvent> RecurringEvents = new List<GameEvent>();

    public void LateInit()
    {
        HealthManager.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        StopAllCoroutines();
    }

    public void HandleEvent(GameEvent e)
    {
        if (e.Recurs > 0)
        {
            if (GetRecurringEventOfSignal(e.Type) != null)
            {
                return;
            }
            else
            {
                RecurringEvents.Add(e);
                StartCoroutine(RecurEvent(e));
            }
        }
    }

    IEnumerator RecurEvent(GameEvent e)
    {
        while (e.Recurs > 0 && HealthManager.IsAlive())
        {
            yield return new WaitForSeconds(recurrenceTime);
            e.Recurs--;
            MyTarget.HandleEvent(GameEvent.CopyEvent(e));
        }
        RecurringEvents.Remove(e);
    }

    public bool AlreadyHandlingEvent(SignalType s)
    {
        return GetRecurringEventOfSignal(s) != null;
    }

    private GameEvent GetRecurringEventOfSignal(SignalType s)
    {
        foreach (GameEvent e in RecurringEvents)
        {
            if (e.Type == s)
            {
                return e; 
            }
        }
        return null;
    }
}
