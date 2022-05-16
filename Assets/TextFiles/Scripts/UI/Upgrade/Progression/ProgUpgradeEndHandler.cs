using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgUpgradeEndHandler : DragEndHandler, Dependency<ModuleGrid>
{
    [SerializeField] TalentGetter Talent;

    public override void HandleDragEnds()
    {
        (Vector3 gridPos, float dist) = ModuleGrid.GetNearestGridItem(transform.position);

        if (dist < Draggable.SNAP_DIST)
        {
            if (ModuleGrid.CanUpgradeBeApplied(gridPos, Talent.Policy.Parent))
            {
                if (ModuleGrid.CanAddTalent(Talent.Policy))
                {
                    ModuleGrid.ApplyUpgrade(Talent.Policy);
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    ModuleGrid.OnFailedPurchase();
                }
            }
        }
    }

    private ProgressionModuleGrid ModuleGrid;

    public void InjectDependency(ModuleGrid dependency)
    {
        ModuleGrid = (ProgressionModuleGrid)dependency;
    }
}
