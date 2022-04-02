using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModifierList : MonoBehaviour
{
    private List<GameEvent> modifiers = new List<GameEvent>();

    public List<GameEvent> GetAttackModifiers()
    {
        return modifiers; 
    }

    public void RemoveAttackModifier(GameEvent e)
    {
        modifiers.Remove(e);
    }

    public void AddAttackModifier(GameEvent e)
    {
        modifiers.Add(e);
    }
}
