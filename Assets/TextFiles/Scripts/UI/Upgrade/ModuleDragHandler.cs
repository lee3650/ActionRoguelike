using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ModuleDragHandler : DragHandler, Dependency<ModuleGrid>
{
    [SerializeField] TalentGetter Talent;
    [SerializeField] Color ValidColor;
    [SerializeField] Color InvalidColor;
    [SerializeField] Color DefaultColor;

    public override void HandleDrag()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.IsPositionValid(gridPos, Talent.Policy))
            {
                Draggable.SetChildrenColor(ValidColor, transform);
            }
            else
            {
                Draggable.SetChildrenColor(InvalidColor, transform);
            }
        }
        else
        {
            Draggable.SetChildrenColor(DefaultColor, transform);
        }
    }

    private ModuleGrid ModuleGrid;

    public void InjectDependency(ModuleGrid mg)
    {
        ModuleGrid = mg;
    }
}