using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEndDragHandler : DragEndHandler, Dependency<ModuleGrid>
{
    [SerializeField] TalentGetter Talent;

    ModuleGrid ModuleGrid;
    
    public override void HandleDragEnds()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.CanUpgradeBeApplied(gridPos, Talent.Policy.Parent))
            {
                ModuleGrid.ApplyUpgrade(Talent.Policy);
                Destroy(gameObject);
                return;
            }
        }
    }

    public void InjectDependency(ModuleGrid dependency)
    {
        ModuleGrid = dependency; 
    }
}