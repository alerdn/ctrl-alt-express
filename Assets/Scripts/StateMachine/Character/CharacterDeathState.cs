using UnityEngine;

public class CharacterDeathState : CharacterBaseState
{
    private readonly int DeathHash = Animator.StringToHash("Death");

    public CharacterDeathState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime(DeathHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}