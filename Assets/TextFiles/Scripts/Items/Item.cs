using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite mySprite;
    [SerializeField] protected string title;

    public List<ItemAction> ValidActions;

    public event Action ItemActionsChanged = delegate { }; 

    public virtual Sprite GetSprite()
    {
        return mySprite; 
    }

    public virtual string GetItemTitle()
    {
        return title; 
    }

    public abstract string GetDescription();
}
