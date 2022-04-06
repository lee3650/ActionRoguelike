using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class GearManager : AbstractGearManager, Initializable
{
    [SerializeField] TalentManager TalentManager;

    private List<Gear> HeldGear = new List<Gear>();
    private List<Item> CorrespondingItems = new List<Item>();

    public override List<ItemType> ItemTypes
    {
        get
        {
            return new List<ItemType>() { ItemType.Amulet, ItemType.Armor, ItemType.Ring };
        }
    }

    public override List<Item> GetItems()
    {
        return CorrespondingItems; 
    }

    protected override void PartialPerformAction(Item item, ItemAction action)
    {
        Gear g = item.GetComponent<Gear>();

        switch (action)
        {
            case ItemAction.Equip:
                TalentManager.ApplyTalent(g.GetPolicy());
                break;
            case ItemAction.Dequip:
                g.GetPolicy().UndoPolicy();
                break;
        }
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
}
