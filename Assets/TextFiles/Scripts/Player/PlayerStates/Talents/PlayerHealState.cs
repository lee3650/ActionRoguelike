using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : State, Talent, Dependency<PlayerInput>, Dependency<MovementController>, Dependency<PlayerMoveState>, Dependency<HealthManager>, Dependency<ManaManager>, Dependency<ActiveTalentManager>, Dependency<PlayerRoomSetter>
{
    [SerializeField] float HealAmt;
    [SerializeField] float HealLength;
    [SerializeField] float SpeedModifier;

    ManaManager ManaManager;
    private HealthManager hm; 
    private PlayerMoveState moveState;
    private PlayerInput PlayerInput;
    private MovementController MovementController;
    private ActiveTalentManager ActiveTalentManager;
    private PlayerRoomSetter PlayerRoomSetter;

    public bool CanUseTalent()
    {
        return ManaManager.ChargesRemaining(1) && hm.GetHealthPercentage() < 0.995f && !PlayerRoomSetter.RoomClear;
    }

    public void InjectDependency(PlayerRoomSetter prs)
    {
        PlayerRoomSetter = prs;
    }

    public void InjectDependency(ActiveTalentManager atm)
    {
        ActiveTalentManager = atm; 
    }

    public void InjectDependency(MovementController mm)
    {
        MovementController = mm; 
    }

    public void InjectDependency(ManaManager mm)
    {
        ManaManager = mm;
    }

    public void InjectDependency(HealthManager h)
    {
        hm = h;
    }

    public void InjectDependency(PlayerMoveState ms)
    {
        moveState = ms; 
    }

    public void InjectDependency(PlayerInput pi)
    {
        PlayerInput = pi; 
    }

    float timer = 0f; 

    public void ModifyMoveSpeedInHeal(float multiplier)
    {
        SpeedModifier *= multiplier;
    }

    public void ModifyHealAmt(float amt)
    {
        HealAmt += amt; 
    }

    public void ModifyHealLength(float multiplier)
    {
        HealLength *= multiplier; 
    }

    private int talentIndex; 

    public override void EnterState()
    {
        timer = 0f;
        hm.DamageTaken += DamageTaken;
        MovementController.ModifyMoveSpeed(SpeedModifier);
        talentIndex = ActiveTalentManager.GetTalentIndex(this);
    }

    private void DamageTaken()
    {
        StateController.EnterState(moveState);
    }

    public override void UpdateState()
    {
        timer += Time.fixedDeltaTime;

        ManaManager.LerpCharge(HealLength, Time.fixedDeltaTime);

        MovementController.MoveInDirection(PlayerInput.GetDirectionalInput());

        if (PlayerInput.GetTalentToActivate() != talentIndex)
        {
            StateController.EnterState(moveState);
            return; 
        }

        if (timer >= HealLength)
        {
            hm.Heal(HealAmt);
            StateController.EnterState(moveState);
        }
    }

    public override void ExitState()
    {
        MovementController.ResetMoveSpeed();
        hm.DamageTaken -= DamageTaken;
    }
}
