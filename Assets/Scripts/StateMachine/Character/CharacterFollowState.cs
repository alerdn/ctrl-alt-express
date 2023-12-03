using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterFollowState : CharacterBaseState
{
    private NavMeshAgent _agent;
    private Transform _otherCharacter;

    public CharacterFollowState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _agent = stateMachine.Character.NavMeshAgent;
        _otherCharacter = stateMachine.OtherCharacter.transform;

        _agent.enabled = true;
        stateMachine.Character.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
        }

        Vector3 newDestination = _otherCharacter.position;
        float distance = Vector3.Distance(_agent.transform.position, _otherCharacter.position);

        if (_agent.enabled && newDestination != _agent.destination && distance > 4f)
        {
            _agent.destination = _otherCharacter.position;
        }

        float animationBlend = _agent.velocity.normalized.magnitude > .1 ? 1f : 0f;
        stateMachine.Character.Animator.SetFloat(FreeLookSpeedHash, animationBlend, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        _agent.ResetPath();
        _agent.enabled = false;
    }
}
