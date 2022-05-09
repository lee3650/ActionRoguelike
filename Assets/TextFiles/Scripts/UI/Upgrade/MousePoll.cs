using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class MousePoll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool MousedOver
    {
        get;
        private set; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MousedOver = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MousedOver = false; 
    }
}
