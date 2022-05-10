using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleEndDragHandler : DragEndHandler, Dependency<ModuleGrid>
{
    ModuleGrid ModuleGrid;
    [SerializeField] TalentGetter Talent;

    public override void HandleDragEnds()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.IsPositionValid(gridPos, Talent.Policy))
            {
                ModuleGrid.WriteToGrid(Talent.Policy, gridPos);
                Destroy(gameObject);
                print("post destroy?");
                return;
            }
        }
    }

    public void InjectDependency(ModuleGrid dependency)
    {
        ModuleGrid = dependency; 
    }
}
