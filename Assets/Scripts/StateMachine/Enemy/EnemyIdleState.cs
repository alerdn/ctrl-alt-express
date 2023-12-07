using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int IdleHash = Animator.StringToHash("EnemyIdle");
    private CharacterStateMachine _characterStateMachine;

    public EnemyIdleState(EnemyStateMachine stateMachine, CharacterStateMachine characterStateMachine) : base(stateMachine)
    {
        _characterStateMachine = characterStateMachine;
    }

    public override void Enter()
    {
        stateMachine.Health.OnDamage += OnDamage;
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!_characterStateMachine.BondStateMachine.Intangible)
        {
            stateMachine.SwitchState(new EnemyPersuingState(stateMachine, stateMachine.GetClosestCharacter()));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.OnDamage -= OnDamage;
    }
}