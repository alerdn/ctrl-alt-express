using System.Collections;
using UnityEngine;

public class EnemyStunnedState : EnemyBaseState
{
    private int[] _animations;

    public EnemyStunnedState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _animations = new int[]
        {
            Animator.StringToHash("Hitted1"),
            Animator.StringToHash("Hitted2"),
            Animator.StringToHash("Hitted3"),
        };
    }

    public override void Enter()
    {
        stateMachine.StartCoroutine(Stun());
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }

    private IEnumerator Stun()
    {;
        stateMachine.Animator.CrossFadeInFixedTime(_animations.GetRandom(), .1f);
        yield return new WaitForSeconds(1.5f);
        stateMachine.SwitchState(new EnemyPersuingState(stateMachine, stateMachine.GetClosestCharacter()));
    }
}