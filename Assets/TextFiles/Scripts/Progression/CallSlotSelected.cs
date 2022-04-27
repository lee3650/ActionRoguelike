using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallSlotSelected : MonoBehaviour, IPointerClickHandler, Initializable
{
    [SerializeField] GearSelectManager GearSelectManager;
    [SerializeField] ItemType MyItemType;
    private ItemDisplayer ItemDisplayer;

    public void Init()
    {
        ItemDisplayer = GetComponent<ItemDisplayer>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (!ItemDisplayer.HasItem())
        {
            GearSelectManager.SlotSelected(MyItemType);
        }
    }
}
