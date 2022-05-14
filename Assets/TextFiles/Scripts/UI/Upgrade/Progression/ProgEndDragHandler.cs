using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgEndDragHandler : DragEndHandler, Dependency<ModuleGrid>
{
    [SerializeField] TalentGetter Talent;

    private ProgressionModuleGrid ModuleGrid;

    public void InjectDependency(ModuleGrid dependency)
    {
        ModuleGrid = dependency as ProgressionModuleGrid;
    }

    public override void HandleDragEnds()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.IsPositionValid(gridPos, Talent.Policy))
            {
                if (ModuleGrid.CanAddTalent(Talent.Policy))
                {
                    ModuleGrid.WriteToGrid(Talent.Policy, gridPos);
                    Destroy(gameObject);
                    print("post destroy?");
                    return;
                }
                else
                {
                    ModuleGrid.OnFailedPurchase();
                }
            }
        }
    }
}
