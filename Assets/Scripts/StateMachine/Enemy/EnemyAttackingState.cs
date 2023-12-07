using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackingState : EnemyBaseState
{
    private Attack _attack;
    private CharacterStateMachine _characterStateMachine;

    public EnemyAttackingState(EnemyStateMachine stateMachine, int attackIndex, CharacterStateMachine characterStateMachine) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
        _characterStateMachine = characterStateMachine;
    }

    public override void Enter()
    {
        foreach (AttackDamage weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(_attack.Damage);
        }
        stateMachine.Health.OnDamage += OnDamage;
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (Vector3.Distance(stateMachine.transform.position, _characterStateMachine.transform.position) > stateMachine.NavMeshAgent.stoppingDistance)
        {
            stateMachine.SwitchState(new EnemyPersuingState(stateMachine, _characterStateMachine));
            return;
        }
        if (_characterStateMachine.BondStateMachine.Intangible)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine, _characterStateMachine));
            return;
        }


        float normalizedTime = stateMachine.GetNormalizedTime("Attack");
        TryComboAttack(normalizedTime);

        stateMachine.transform.LookAt(_characterStateMachine.transform.position);
    }

    public override void Exit()
    {
        stateMachine.Health.OnDamage -= OnDamage;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (normalizedTime < _attack.ComboAttackTime) return;

        int comboStateIndex = _attack.ComboStateIndex;
        if (_attack.ComboStateIndex == -1) comboStateIndex = 0;

        stateMachine.SwitchState(new EnemyAttackingState(stateMachine, comboStateIndex, _characterStateMachine));
    }
}
