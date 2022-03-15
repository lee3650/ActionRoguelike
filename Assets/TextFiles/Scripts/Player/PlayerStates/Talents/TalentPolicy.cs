using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalentPolicy : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] bool reusable;
    [SerializeField] private bool upgradable = false;

    public bool Reusable
    {
        get
        {
            return reusable;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public abstract void ApplyPolicy();
    public virtual TalentPolicy GetNextUpgrade()
    {
        return null; 
    }

    public virtual void AppliedNextUpgrade()
    {
    }

    public bool Upgradable
    {
        get
        {
            return upgradable;
        }
        protected set
        {
            upgradable = value; 
        }
    }
}
