using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

[RequireComponent(typeof(RectTransform))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, LateInitializable, Dependency<ModuleGrid>, Dependency<RectTransform>
{
    [SerializeField] DragHandler DragHandler;
    [SerializeField] DragEndHandler DragEndHandler;
    [SerializeField] Color DefaultColor;

    public const float SNAP_DIST = 100f;

    private Transform group;

    private RectTransform Canvas;

    private RectTransform MyRect;

    private ModuleGrid ModuleGrid;

    public void InjectDependency(ModuleGrid g)
    {
        ModuleGrid = g;
    }

    public void InjectDependency(RectTransform dependency)
    {
        Canvas = dependency;
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
            if (dist < SNAP_DIST)
            {
                MyRect.position = gridPos;
            }

            DragHandler.HandleDrag();
        }
    }

    public static void SetChildrenColor(Color c, Transform transform)
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent<Image>(out Image i))
            {
                i.color = c;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEndHandler.HandleDragEnds();
        transform.parent = group;
        SetChildrenColor(DefaultColor, transform);
    }
}