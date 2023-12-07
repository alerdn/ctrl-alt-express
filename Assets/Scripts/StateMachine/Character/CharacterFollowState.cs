using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CharacterFollowState : CharacterBaseState
{
    private NavMeshAgent _agent;
    private Transform _target;
    private EnemyStateMachine _closestEnemy;

    public CharacterFollowState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _agent = stateMachine.Character.NavMeshAgent;
        _target = stateMachine.OtherCharacter.transform;

        ChooseTarget();

        stateMachine.transform.Translate(Vector3.up * .2f);
        stateMachine.Character.Animator.CrossFadeInFixedTime(stateMachine.Character.FreeLookBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
            return;
        }

        try
        {
            if (_closestEnemy != null && _target == _closestEnemy.transform)
            {
                if (Vector3.Distance(_agent.transform.position, _target.position) <= _agent.stoppingDistance)
                {
                    stateMachine.SwitchState(new CharacterAttackingState(stateMachine, 0, true, _closestEnemy.transform));
                    return;
                }
            }

            Vector3 newDestination = _target.position;
            float distance = Vector3.Distance(_agent.transform.position, _target.position);
            if (_agent.enabled && _target == stateMachine.OtherCharacter.transform && distance <= 3f)
            {
                _agent.ResetPath();
            }

            if (_agent.enabled && newDestination != _agent.destination)
            {
                if ((_target == stateMachine.OtherCharacter.transform && distance > 4f) ||
                    (_closestEnemy != null && _target == _closestEnemy.transform))
                {
                    _agent.destination = _target.position;
                }

                float animationBlend = _agent.velocity.normalized.magnitude > .1 ? 1f : 0f;
                stateMachine.Character.Animator.SetFloat(FreeLookSpeedHash, animationBlend, AnimatorDampTime, deltaTime);
            }

            ChooseTarget();
        }
        catch
        {
            _target = stateMachine.OtherCharacter.transform;
        }
    }

    public override void Exit()
    {
        _agent.ResetPath();
    }

    public EnemyStateMachine GetClosestEnenmy()
    {
        GameObject[] enemiesObj = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyStateMachine[] enemies = enemiesObj.ToList().Select(x => x.GetComponent<EnemyStateMachine>()).ToArray();
        if (enemies.Length == 0) return null;

        var closestEnemy = enemies[0];
        var closestDistance = Vector3.Distance(stateMachine.transform.position, closestEnemy.transform.position);

        foreach (var character in enemies)
        {
            var distance = Vector3.Distance(stateMachine.transform.position, character.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = character;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    private void ChooseTarget()
    {
        _closestEnemy = GetClosestEnenmy();
        if (_closestEnemy != null)
            _target = _closestEnemy.transform;

        if (Vector3.Distance(_agent.transform.position, _target.position) > 10f ||
            Vector3.Distance(_agent.transform.position, stateMachine.OtherCharacter.transform.position) > 10f)
        {
            _target = stateMachine.OtherCharacter.transform;
        }
    }
}
