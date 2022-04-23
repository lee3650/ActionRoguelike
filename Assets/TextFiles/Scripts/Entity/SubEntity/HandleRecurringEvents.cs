using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRecurringEvents : MonoBehaviour, SubEntity, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] GenericTarget MyTarget;
    private List<GameEvent> RecurringEvents = new List<GameEvent>();

    public const float RECUR_TIME = 0.75f;

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
        if (e.HasStat(GameEvent.RepeatingKey))
        {
            if (GetRecurringEventOfSignal(e.Type) != null)
            {
                return; 
            }

            RecurringEvents.Add(e);
            e.RemoveStat(GameEvent.RepeatingKey);
            StartCoroutine(RecurEvent(e));
        }
    }

    IEnumerator RecurEvent(GameEvent e)
    {
        while (e.GetStatAsFloat(GameEvent.RecursKey, 0) > 0 && HealthManager.IsAlive())
        {
            yield return new WaitForSeconds(RECUR_TIME);
            e.AddToStat(GameEvent.RecursKey, -1);
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
