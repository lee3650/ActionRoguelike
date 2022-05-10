using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDragHandler : DragHandler, Dependency<ModuleGrid>
{
    [SerializeField] TalentGetter Talent;
    [SerializeField] Color ValidColor;
    [SerializeField] Color InvalidColor;
    [SerializeField] Color DefaultColor;

    ModuleGrid ModuleGrid;
    
    public override void HandleDrag()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.CanUpgradeBeApplied(gridPos, Talent.Policy.Parent))
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

    public void InjectDependency(ModuleGrid dependency)
    {
        ModuleGrid = dependency;
    }
}
