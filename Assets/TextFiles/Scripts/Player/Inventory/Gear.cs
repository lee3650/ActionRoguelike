using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour, Initializable, StatSupplier, LateInitializable
{
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] TalentPolicy PolicyPrefab;
    [SerializeField] Item Item;
    [SerializeField] List<ItemAction> EquippedActions;
    [SerializeField] List<ItemAction> DequippedActions;
    private TalentPolicy ActualPolicy = null;
    private bool allowsPickup = true;

    public GearType GearType;

    private bool equipped = false;

    public bool Equipped
    {
        get
        {
            return equipped; 
        }
        set
        {
            equipped = value;
            if (equipped)
            {
                Item.SetValidActions(EquippedActions);
            } else
            {
                Item.SetValidActions(DequippedActions);
            }
            Item.InvokeItemModified();
        }
    }

    public TalentPolicy GetPolicy()
    {
        return ActualPolicy; 
    }

    public bool AllowsPickup()
    {
        return allowsPickup;
    }

    public void OnPickup(Transform t)
    {
        SpriteRenderer.enabled = false; 
        allowsPickup = false;
        transform.SetParent(t);
        transform.localPosition = Vector3.zero; 
    }

    public void Init()
    {
        ActualPolicy = Instantiate(PolicyPrefab);
    }

    public void LateInit()
    {
        Item.SetValidActions(DequippedActions);
    }

    public (string, string)[] GetStats()
    {
        return new (string, string)[] { ("Equipped", "" + Equipped) };
    }
}
