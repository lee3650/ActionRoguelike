using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public abstract class Item : MonoBehaviour
{
    [SerializeField] bool overrideDefaultSprite;
    [SerializeField] private Sprite mySprite;
    [SerializeField] bool overrideTitle;
    [SerializeField] protected string title;

    public List<ItemAction> ValidActions;

    public event Action ItemModified = delegate { }; 

    public void InvokeItemModified()
    {
        ItemModified();
    }

    public void SetValidActions(List<ItemAction> actions)
    {
        ValidActions = actions;
    }
    
    public virtual Sprite GetSprite()
    {
        if (overrideDefaultSprite)
        {
            return mySprite; 
        }
        return GetComponent<SpriteRenderer>().sprite; 
    }

    public virtual string GetItemTitle()
    {
        if (overrideTitle)
        {
            return title; 
        }
        return name; 
    }

    public abstract string GetDescription();
}
