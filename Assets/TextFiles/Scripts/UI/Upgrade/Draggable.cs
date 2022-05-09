using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, LateInitializable
{
    [SerializeField] RectTransform Canvas;
    [SerializeField] ModuleGrid ModuleGrid;

    private Transform group;

    private RectTransform MyRect;

    public void SetCanvas(RectTransform canv)
    {
        Canvas = canv; 
    }

    public void LateInit()
    {
        group = transform.parent;
        MyRect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = Canvas;
        SetDragPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDragPosition(eventData);
    }

    private void SetDragPosition(PointerEventData data)
    {
        Vector3 globalPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(Canvas, data.position, data.pressEventCamera, out globalPos))
        {
            MyRect.position = globalPos;

            (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(globalPos);
            if (dist < 100f)
            {
                MyRect.position = gridPos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.parent = group; 
    }
}