public class CharacterSpecialAttackingState : CharacterBaseState
{
    private AbilityBase _ability;

    public CharacterSpecialAttackingState(CharacterStateMachine stateMachine, int abilityIndex) : base(stateMachine)
    {
        _ability = stateMachine.Abilities[abilityIndex];
    }

    public override void Enter()
    {
        _ability.OnAbilityFinish += OnFinishAbility;
        _ability.UseAbility(stateMachine);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
        _ability.OnAbilityFinish -= OnFinishAbility;
    }

    private void OnFinishAbility()
    {
        if (stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new CharacterFollowState(stateMachine));
        }
    }
}
