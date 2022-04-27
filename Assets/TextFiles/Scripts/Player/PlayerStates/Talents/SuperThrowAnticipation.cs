using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperThrowAnticipation : AbstractAnticipation, Talent, Dependency<HandAndArmGetter>, Dependency<ReversedTracker>, Dependency<ManaManager>, Dependency<PlayerRoomSetter>, Dependency<WeaponManager>
{
    public void InjectDependency(HandAndArmGetter hm)
    {
        HandAndArm = hm;
    }

    WeaponManager wm;

    public void InjectDependency(WeaponManager wm)
    {
        this.wm = wm; 
    }

    private PlayerRoomSetter prs;

    public void InjectDependency(PlayerRoomSetter prs)
    {
        this.prs = prs;
    }

    public void InjectDependency(ReversedTracker rt)
    {
        ReversedTracker = rt;
    }

    private ManaManager manaManager;

    public bool CanUseTalent()
    {
        return manaManager.ChargesRemaining(1) && wm.DoesCurrentWeaponAllowAction(ActionStrings.SuperThrow);
    }

    public void InjectDependency(ManaManager mm)
    {
        manaManager = mm;
    }

    public override void EnterState()
    {
        SetupState();
        manaManager.UseCharge();
    }

    public override void UpdateState()
    {
        PartialUpdate();
    }

    public override void ExitState()
    {

    }
}
