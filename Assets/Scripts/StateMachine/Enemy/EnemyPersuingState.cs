using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPersuingState : EnemyBaseState
{
    private CharacterStateMachine _closestCharacter;
    private NavMeshAgent _agent;

    public EnemyPersuingState(EnemyStateMachine stateMachine, CharacterStateMachine closestCharacter) : base(stateMachine)
    {
        _closestCharacter = closestCharacter;
    }

    public override void Enter()
    {
        _agent = stateMachine.NavMeshAgent;
        stateMachine.Health.OnDamage += OnDamage;
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
        if (Vector3.Distance(_agent.transform.position, _closestCharacter.transform.position) <= _agent.stoppingDistance)
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine, 0, _closestCharacter));
            return;
        }
        if (_closestCharacter.BondStateMachine.Intangible)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine, _closestCharacter));
            return;
        }

        Vector3 newDestination = _closestCharacter.transform.position;
        if (_agent.enabled && newDestination != _agent.destination)
        {
            _agent.destination = _closestCharacter.transform.position;
        }

        float animationBlend = _agent.velocity.normalized.magnitude > .1 ? 1f : 0f;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, animationBlend, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Health.OnDamage -= OnDamage;
        _agent.ResetPath();
    }
}
