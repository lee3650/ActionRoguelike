using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class GearManager : MonoBehaviour, ItemSupplier, Initializable
{
    [SerializeField] TalentManager TalentManager;

    private List<Gear> HeldGear = new List<Gear>();
    private List<Item> CorrespondingItems = new List<Item>();

    private Dictionary<GearType, Gear> EquippedGear = new Dictionary<GearType, Gear>();

    public event Action<GearType, Gear> GearEquipped = delegate { };

    public void Init()
    {
        foreach (GearType t in Enum.GetValues(typeof(GearType)))
        {
            EquippedGear[t] = null; 
        }
    }

    public bool HasEquippedGear(GearType t)
    {
        return EquippedGear[t] != null; 
    }

    public Gear GetEquippedGear(GearType t)
    {
        return EquippedGear[t];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Gear>(out Gear g))
        {
            if (g.AllowsPickup())
            {
                g.OnPickup(transform);
            }
            HeldGear.Add(g);
            CorrespondingItems.Add(g.GetComponent<Item>());
        }
    }

    public void PerformActionOnItem(Item item, ItemAction action)
    {
        Gear g = item.GetComponent<Gear>();

        switch (action)
        {
            case ItemAction.Equip:
                Debug.Assert(g != null);

                if (EquippedGear[g.GearType] != null)
                {
                    PerformActionOnItem(EquippedGear[g.GearType].GetComponent<Item>(), ItemAction.Dequip);
                }

                EquippedGear[g.GearType] = g;
                GearEquipped(g.GearType, g);
                g.Equipped = true;
                TalentManager.ApplyTalent(g.GetPolicy());
                break;
            case ItemAction.Dequip:
                EquippedGear[g.GearType] = null;
                g.GetPolicy().UndoPolicy();
                GearEquipped(g.GearType, null);
                g.Equipped = false;
                break; 

        }
    }

    public ItemType ItemType
    {
        get
        {
            return ItemType.Gear; 
        }
    }

    public List<Item> GetItems()
    {
        return CorrespondingItems; 
    }
}
