using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class SendMouseEvents : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] GridDS Grid;

    private bool mousedOver = false;

    private GridElement MousedOverElement = null;

    void Update()
    {
        if (mousedOver)
        {
            Vector3 mousePos = Input.mousePosition; //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GridElement nearest = Grid.FindNearestGridElement(mousePos);

            if (nearest != null)
            {
                if (nearest != MousedOverElement)
                {
                    nearest.HandleEvent(GridElementEvent.MouseEnter);

                    if (MousedOverElement != null)
                    {
                        MousedOverElement.HandleEvent(GridElementEvent.MouseExit);
                    }
                }
            }
            else
            {
                if (MousedOverElement != null)
                {
                    MousedOverElement.HandleEvent(GridElementEvent.MouseExit);
                }
            }

            MousedOverElement = nearest;

        }
        else
        {
            if (MousedOverElement != null)
            {
                MousedOverElement.HandleEvent(GridElementEvent.MouseExit);
                MousedOverElement = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mousedOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mousedOver = false;
    }
}
